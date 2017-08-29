using System;
using System.Collections.Generic;

namespace LinqAF.Impl
{
    static class EmptyCache<TItem>
    {
        public static EmptyEnumerable<TItem> Empty = new EmptyEnumerable<TItem>(Enumerable.EmptySigil);
        public static EmptyOrderedEnumerable<TItem> EmptyOrdered = new EmptyOrderedEnumerable<TItem>(Enumerable.EmptyOrderedSigil);
        
        public static BoxedEnumerable<TItem> BadBoxedEmpty = new BoxedEnumerable<TItem>(new BadBoxedEmptyImpl());
        public static BoxedEnumerable<TItem> BoxedEmpty = new BoxedEnumerable<TItem>(new BoxedEmptyImpl());

        public static EmptyComparer<TItem> EmptyComparer = new EmptyComparer<TItem>(Enumerable.EmptyComparerSigil);

        // intentionally not using Allocator.Current since this is to never be modified or released
        public static List<TItem> List = new List<TItem>(0);
        public static TItem[] Array = new TItem[0];

        class BadBoxedEmptyImpl : IBoxedEnumerable<TItem>
        {
            public IBoxedEnumerator<TItem> GetEnumerator()
            {
                throw CommonImplementation.InnerUninitialized();
            }

            public bool IsDefaultValue() => true;
        }

        class BoxedEmptyImpl: IBoxedEnumerable<TItem>
        {
            static Enumerator EmptyEnumerator = new Enumerator();

            class Enumerator : IBoxedEnumerator<TItem>
            {
                public TItem Current => default(TItem);

                public bool IsDefaultValue() => false;

                public void Dispose() { }

                public bool MoveNext() => false;

                public void Reset() { }
            }

            public IBoxedEnumerator<TItem> GetEnumerator() => EmptyEnumerator;

            public bool IsDefaultValue() => false;
        }
    }

    static class EmptyCache<TItem1, TItem2>
    {
        // intentionally not using Allocator.Current since these are never to be modified or released
        static IndexedItemContainer<TItem2> EmptyContainer = new IndexedItemContainer<TItem2>(0, new TItem2[0]);
        static LookupHashtable<TItem1, TItem2> EmptyLookupHashtable = new LookupHashtable<TItem1, TItem2>(0, new SlimGrouping<TItem1, TItem2>[0], new LookupBucket<TItem1, TItem2>[0]);

        public static LookupDefaultEnumerable<TItem1, TItem2> EmptyLookupDefault = new LookupDefaultEnumerable<TItem1, TItem2>(ref EmptyLookupHashtable);
        public static LookupSpecificEnumerable<TItem1, TItem2> EmptyLookupSpecific = new LookupSpecificEnumerable<TItem1, TItem2>(ref EmptyLookupHashtable, EqualityComparer<TItem1>.Default);
        public static GroupingEnumerable<TItem1, TItem2> EmptyGrouping = new GroupingEnumerable<TItem1, TItem2>(default(TItem1), 0, new int[0], ref EmptyContainer);
        public static GroupedEnumerable<TItem1, TItem2> EmptyGrouped = new GroupedEnumerable<TItem1, TItem2>(ref EmptyGrouping);
    }
}
