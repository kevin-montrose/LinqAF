using LinqAF.Impl;
using System;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class SequenceEqualMethodsBase :
        ExtensionMethodsBase
    {
        // SequenceEqual default
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefParam(first), ref second, null);
        }
        
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefParam(first), ref second, null);
        }

        public bool SequenceEqual<TGenKey, TGenElement>(
            PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            LookupEnumerable<TGenKey, TGenElement> second
            )
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(RefParam(first), ref second, null);
        }

        // SequenceEqual weird, specific

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefParam(first), ref second, comparer);
        }
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>
                >(RefParam(first), ref second, comparer);
        }
        public bool SequenceEqual<TGenKey, TGenElement>(
            PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>> first,
            LookupEnumerable<TGenKey, TGenElement> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    PlaceholderEnumerable<GroupingEnumerable<TGenKey, TGenElement>>,
                    PlaceholderEnumerator<GroupingEnumerable<TGenKey, TGenElement>>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(RefParam(first), ref second, comparer);
        }

        // SequenceEqual - GroupByDefault, default
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupByDefaultEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, null);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupBySpecificEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, null);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            LookupEnumerable<TGenKey, TGenElement> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(ref first, ref second, null);
        }

        // SequenceEqual - GroupByDefault, specific
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupByDefaultEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, comparer);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupBySpecificEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, comparer);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            LookupEnumerable<TGenKey, TGenElement> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupByDefaultEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(ref first, ref second, comparer);
        }

        // SequenceEqual - GroupBySpecific, default
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupByDefaultEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, null);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupBySpecificEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, null);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            LookupEnumerable<TGenKey, TGenElement> second
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(ref first, ref second, null);
        }

        // SequenceEqual - GroupBySpecific, specific
        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupByDefaultEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, comparer);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupBySpecificEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, comparer);
        }

        public bool SequenceEqual<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>(
            GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator> first,
            LookupEnumerable<TGenKey, TGenElement> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable : struct, IStructEnumerable<TGenInItem, TGenEnumerator>
            where TGenEnumerator : struct, IStructEnumerator<TGenInItem>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem, TGenKey, TGenElement, TGenEnumerable, TGenEnumerator>,
                    GroupBySpecificEnumerator<TGenInItem, TGenKey, TGenElement, TGenEnumerator>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(ref first, ref second, comparer);
        }

        // SequenceEqual - Lookup, default
        public bool SequenceEqual<TGenKey, TGenElement, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            LookupEnumerable<TGenKey, TGenElement> first,
            GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second
            )
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupByDefaultEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, null);
        }

        public bool SequenceEqual<TGenKey, TGenElement, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            LookupEnumerable<TGenKey, TGenElement> first,
            GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second
            )
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupBySpecificEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, null);
        }

        public bool SequenceEqual<TGenKey, TGenElement>(
            LookupEnumerable<TGenKey, TGenElement> first,
            LookupEnumerable<TGenKey, TGenElement> second
            )
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(ref first, ref second, null);
        }

        // SequenceEqual - Lookup, specific
        public bool SequenceEqual<TGenKey, TGenElement, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            LookupEnumerable<TGenKey, TGenElement> first,
            GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>,
                    GroupByDefaultEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupByDefaultEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, comparer);
        }

        public bool SequenceEqual<TGenKey, TGenElement, TGenInItem2, TGenEnumerable2, TGenEnumerator2>(
            LookupEnumerable<TGenKey, TGenElement> first,
            GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
            where TGenEnumerable2 : struct, IStructEnumerable<TGenInItem2, TGenEnumerator2>
            where TGenEnumerator2 : struct, IStructEnumerator<TGenInItem2>
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>,
                    GroupBySpecificEnumerable<TGenInItem2, TGenKey, TGenElement, TGenEnumerable2, TGenEnumerator2>,
                    GroupBySpecificEnumerator<TGenInItem2, TGenKey, TGenElement, TGenEnumerator2>
                >(ref first, ref second, comparer);
        }

        public bool SequenceEqual<TGenKey, TGenElement>(
            LookupEnumerable<TGenKey, TGenElement> first,
            LookupEnumerable<TGenKey, TGenElement> second,
            IEqualityComparer<GroupingEnumerable<TGenKey, TGenElement>> comparer
            )
        {
            return
                CommonImplementation.SequenceEqual<
                    GroupingEnumerable<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>,
                    LookupEnumerable<TGenKey, TGenElement>,
                    LookupEnumerator<TGenKey, TGenElement>
                >(ref first, ref second, comparer);
        }
    }
}