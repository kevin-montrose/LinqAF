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

        public static List<TItem> List = new List<TItem>(0);

        class BadBoxedEmptyImpl : IBoxedEnumerable<TItem>
        {
            public IBoxedEnumerator<TItem> GetEnumerator()
            {
                throw new InvalidOperationException("Inner enumerable is uninitialized");
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
        public static LookupEnumerable<TItem1, TItem2> EmptyLookup = new LookupEnumerable<TItem1, TItem2>(new List<TItem1>(0), new Dictionary<TItem1, GroupingEnumerable<TItem1, TItem2>>(0), null);
        public static GroupingEnumerable<TItem1, TItem2> EmptyGrouping = new GroupingEnumerable<TItem1, TItem2>(default(TItem1), new List<TItem2>(0));
    }
}
