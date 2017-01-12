using LinqAF.Impl;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqAF
{
    partial struct EmptyOrderedEnumerable<TItem>
    {
        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemDefaultOrderedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemDefaultOrderedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemSpecificOrderedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemSpecificOrderedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem>(IEnumerable inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<object, TJoin_KeyItem> innerKeySelector, Func<TItem, object, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem>(IEnumerable inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<object, TJoin_KeyItem> innerKeySelector, Func<TItem, object, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_RightItem, TJoin_KeyItem>(EmptyOrderedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_RightItem, TJoin_KeyItem>(EmptyEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(IEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(LinkedList<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(Queue<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(ReverseRangeEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(Stack<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(SortedSet<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(RepeatEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(RangeEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(List<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(TJoin_RightItem[] inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(BoxedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(IEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(ReverseRangeEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(LinkedList<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(TJoin_RightItem[] inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_RightItem, TJoin_KeyItem>(EmptyOrderedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(Stack<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(Queue<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(RepeatEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(RangeEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(SortedSet<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(List<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_RightItem, TJoin_KeyItem>(EmptyEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(BoxedEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryValue>(Dictionary<TJoin_RightItem, TJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupedKey>(GroupingEnumerable<TJoin_GroupedKey, TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryKey>(Dictionary<TJoin_DictionaryKey, TJoin_RightItem>.ValueCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryValue>(SortedDictionary<TJoin_RightItem, TJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_LookupKey, TJoin_LookupElement>(LookupEnumerable<TJoin_LookupKey, TJoin_LookupElement> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TJoin_LookupKey, TJoin_LookupElement>, TJoin_KeyItem> innerKeySelector, Func<TItem, GroupingEnumerable<TJoin_LookupKey, TJoin_LookupElement>, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryKey>(SortedDictionary<TJoin_DictionaryKey, TJoin_RightItem>.ValueCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_DictionaryKey, TJoin_DictionaryValue>(SortedDictionary<TJoin_DictionaryKey, TJoin_DictionaryValue> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_KeyItem> innerKeySelector, Func<TItem, KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_DictionaryKey, TJoin_DictionaryValue>(Dictionary<TJoin_DictionaryKey, TJoin_DictionaryValue> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_KeyItem> innerKeySelector, Func<TItem, KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupedKey>(GroupedEnumerable<TJoin_GroupedKey, TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryValue>(Dictionary<TJoin_RightItem, TJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_DictionaryKey, TJoin_DictionaryValue>(Dictionary<TJoin_DictionaryKey, TJoin_DictionaryValue> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_KeyItem> innerKeySelector, Func<TItem, KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryKey>(SortedDictionary<TJoin_DictionaryKey, TJoin_RightItem>.ValueCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryValue>(SortedDictionary<TJoin_RightItem, TJoin_DictionaryValue>.KeyCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupedKey>(GroupingEnumerable<TJoin_GroupedKey, TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DictionaryKey>(Dictionary<TJoin_DictionaryKey, TJoin_RightItem>.ValueCollection inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_LookupKey, TJoin_LookupElement>(LookupEnumerable<TJoin_LookupKey, TJoin_LookupElement> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TJoin_LookupKey, TJoin_LookupElement>, TJoin_KeyItem> innerKeySelector, Func<TItem, GroupingEnumerable<TJoin_LookupKey, TJoin_LookupElement>, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }
        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_DictionaryKey, TJoin_DictionaryValue>(SortedDictionary<TJoin_DictionaryKey, TJoin_DictionaryValue> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_KeyItem> innerKeySelector, Func<TItem, KeyValuePair<TJoin_DictionaryKey, TJoin_DictionaryValue>, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupedKey>(GroupedEnumerable<TJoin_GroupedKey, TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator>(TakeWhileIndexedEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_TakeInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerator>
            where TJoin_TakeInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerator>
            where TJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator>(WhereEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator>(WhereIndexedEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerator>
            where TJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ReverseInnerEnumerable, TJoin_ReverseInnerEnumerator>(ReverseEnumerable<TJoin_RightItem, TJoin_ReverseInnerEnumerable, TJoin_ReverseInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_ReverseInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ReverseInnerEnumerator>
            where TJoin_ReverseInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_BridgeType, TJoin_IdentityEnumerator>(IdentityEnumerable<TJoin_RightItem, TJoin_BridgeType, TJoin_IdentityEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_BridgeType : class
            where TJoin_IdentityEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator>(SkipEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SkipInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerator>
            where TJoin_SkipInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator>(SkipWhileIndexedEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SkipInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerator>
            where TJoin_SkipInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator>(TakeEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_TakeInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerator>
            where TJoin_TakeInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator>(TakeWhileEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_TakeInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerator>
            where TJoin_TakeInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator>(SkipWhileEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SkipInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerator>
            where TJoin_SkipInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerator>
            where TJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerator>
            where TJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>(DistinctSpecificEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerator>
            where TJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator>(SkipEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SkipInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerator>
            where TJoin_SkipInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator>(DistinctDefaultEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerable, TJoin_DistinctInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_DistinctInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DistinctInnerEnumerator>
            where TJoin_DistinctInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptySpecificEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerator>
            where TJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ReverseInnerEnumerable, TJoin_ReverseInnerEnumerator>(ReverseEnumerable<TJoin_RightItem, TJoin_ReverseInnerEnumerable, TJoin_ReverseInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_ReverseInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ReverseInnerEnumerator>
            where TJoin_ReverseInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator>(TakeWhileIndexedEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_TakeInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerator>
            where TJoin_TakeInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator>(TakeWhileEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_TakeInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerator>
            where TJoin_TakeInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator>(WhereIndexedEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator>(WhereEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator>(TakeEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerable, TJoin_TakeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_TakeInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_TakeInnerEnumerator>
            where TJoin_TakeInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_BridgeType, TJoin_IdentityEnumerator>(IdentityEnumerable<TJoin_RightItem, TJoin_BridgeType, TJoin_IdentityEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_BridgeType : class
            where TJoin_IdentityEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator>(SkipWhileIndexedEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SkipInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerator>
            where TJoin_SkipInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator>(SkipWhileEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerable, TJoin_SkipInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SkipInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SkipInnerEnumerator>
            where TJoin_SkipInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator>(DefaultIfEmptyDefaultEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerable, TJoin_DefaultIfEmptyInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_DefaultIfEmptyInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_DefaultIfEmptyInnerEnumerator>
            where TJoin_DefaultIfEmptyInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator>(SelectIndexedEnumerable<TJoin_SelectInItem, TJoin_RightItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_OrderByKey, TJoin_OrderByInnerEnumerable, TJoin_OrderByInnerEnumerator, TJoin_OrderByComparer>(OrderByEnumerable<TJoin_RightItem, TJoin_OrderByKey, TJoin_OrderByInnerEnumerable, TJoin_OrderByInnerEnumerator, TJoin_OrderByComparer> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_OrderByInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_OrderByInnerEnumerator>
            where TJoin_OrderByInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_OrderByComparer : struct, IStructComparer<TJoin_RightItem, TJoin_OrderByKey>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator>(SelectEnumerable<TJoin_SelectInItem, TJoin_RightItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate>(WhereWhereEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_WherePredicate : struct, IStructPredicate<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_OfTypeInItem, TJoin_OfTypeInnerEnumerable, TJoin_OfTypeInnerEnumerator>(OfTypeEnumerable<TJoin_OfTypeInItem, TJoin_RightItem, TJoin_OfTypeInnerEnumerable, TJoin_OfTypeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_OfTypeInnerEnumerable : struct, IStructEnumerable<TJoin_OfTypeInItem, TJoin_OfTypeInnerEnumerator>
            where TJoin_OfTypeInnerEnumerator : struct, IStructEnumerator<TJoin_OfTypeInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_CastInItem, TJoin_KeyItem, TJoin_RightItem, TJoin_CastInnerEnumerable, TJoin_CastInnerEnumerator>(CastEnumerable<TJoin_CastInItem, TJoin_RightItem, TJoin_CastInnerEnumerable, TJoin_CastInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_CastInnerEnumerable : struct, IStructEnumerable<TJoin_CastInItem, TJoin_CastInnerEnumerator>
            where TJoin_CastInnerEnumerator : struct, IStructEnumerator<TJoin_CastInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_OrderByKey, TJoin_OrderByInnerEnumerable, TJoin_OrderByInnerEnumerator, TJoin_OrderByComparer>(OrderByEnumerable<TJoin_RightItem, TJoin_OrderByKey, TJoin_OrderByInnerEnumerable, TJoin_OrderByInnerEnumerator, TJoin_OrderByComparer> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_OrderByInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_OrderByInnerEnumerator>
            where TJoin_OrderByInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_OrderByComparer : struct, IStructComparer<TJoin_RightItem, TJoin_OrderByKey>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator>(SelectIndexedEnumerable<TJoin_SelectInItem, TJoin_RightItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate>(WhereWhereEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_WherePredicate : struct, IStructPredicate<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator>(SelectEnumerable<TJoin_SelectInItem, TJoin_RightItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_OfTypeInItem, TJoin_OfTypeInnerEnumerable, TJoin_OfTypeInnerEnumerator>(OfTypeEnumerable<TJoin_OfTypeInItem, TJoin_RightItem, TJoin_OfTypeInnerEnumerable, TJoin_OfTypeInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_OfTypeInnerEnumerable : struct, IStructEnumerable<TJoin_OfTypeInItem, TJoin_OfTypeInnerEnumerator>
            where TJoin_OfTypeInnerEnumerator : struct, IStructEnumerator<TJoin_OfTypeInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_CastInItem, TJoin_KeyItem, TJoin_RightItem, TJoin_CastInnerEnumerable, TJoin_CastInnerEnumerator>(CastEnumerable<TJoin_CastInItem, TJoin_RightItem, TJoin_CastInnerEnumerable, TJoin_CastInnerEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_CastInnerEnumerable : struct, IStructEnumerable<TJoin_CastInItem, TJoin_CastInnerEnumerator>
            where TJoin_CastInnerEnumerator : struct, IStructEnumerator<TJoin_CastInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator>(UnionSpecificEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_UnionFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerator>
            where TJoin_UnionFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_UnionSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionSecondEnumerator>
            where TJoin_UnionSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator>(ExceptDefaultEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerator>
            where TJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptSecondEnumerator>
            where TJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection>(SelectSelectEnumerable<TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInnerItem>
            where TJoin_SelectProjection : struct, IStructProjection<TJoin_RightItem, TJoin_SelectInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator>(ExceptSpecificEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerator>
            where TJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptSecondEnumerator>
            where TJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupByDefaultEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_KeyItem> innerKeySelector, Func<TItem, GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_OutItem> resultSelector)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator>(IntersectDefaultEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerator>
            where TJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectSecondEnumerator>
            where TJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator>(IntersectSpecificEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerator>
            where TJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectSecondEnumerator>
            where TJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupBySpecificEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_KeyItem> innerKeySelector, Func<TItem, GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_OutItem> resultSelector)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator>(UnionDefaultEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_UnionFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerator>
            where TJoin_UnionFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_UnionSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionSecondEnumerator>
            where TJoin_UnionSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ConcatFirstEnumerable, TJoin_ConcatFirstEnumerator, TJoin_ConcatSecondEnumerable, TJoin_ConcatSecondEnumerator>(ConcatEnumerable<TJoin_RightItem, TJoin_ConcatFirstEnumerable, TJoin_ConcatFirstEnumerator, TJoin_ConcatSecondEnumerable, TJoin_ConcatSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_ConcatFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ConcatFirstEnumerator>
            where TJoin_ConcatFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_ConcatSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ConcatSecondEnumerator>
            where TJoin_ConcatSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator>(ExceptSpecificEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerator>
            where TJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptSecondEnumerator>
            where TJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection>(SelectSelectEnumerable<TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInnerItem>
            where TJoin_SelectProjection : struct, IStructProjection<TJoin_RightItem, TJoin_SelectInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupByDefaultEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_KeyItem> innerKeySelector, Func<TItem, GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator>(ExceptDefaultEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerable, TJoin_ExceptFirstEnumerator, TJoin_ExceptSecondEnumerable, TJoin_ExceptSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_ExceptFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptFirstEnumerator>
            where TJoin_ExceptFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_ExceptSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ExceptSecondEnumerator>
            where TJoin_ExceptSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator>(IntersectSpecificEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerator>
            where TJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectSecondEnumerator>
            where TJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator>(IntersectDefaultEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerable, TJoin_IntersectFirstEnumerator, TJoin_IntersectSecondEnumerable, TJoin_IntersectSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_IntersectFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectFirstEnumerator>
            where TJoin_IntersectFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_IntersectSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_IntersectSecondEnumerator>
            where TJoin_IntersectSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupBySpecificEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_KeyItem> innerKeySelector, Func<TItem, GroupingEnumerable<TJoin_GroupByKey, TJoin_GroupByElement>, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator>(UnionSpecificEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_UnionFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerator>
            where TJoin_UnionFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_UnionSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionSecondEnumerator>
            where TJoin_UnionSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator>(UnionDefaultEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerable, TJoin_UnionFirstEnumerator, TJoin_UnionSecondEnumerable, TJoin_UnionSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_UnionFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionFirstEnumerator>
            where TJoin_UnionFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_UnionSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_UnionSecondEnumerator>
            where TJoin_UnionSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ConcatFirstEnumerable, TJoin_ConcatFirstEnumerator, TJoin_ConcatSecondEnumerable, TJoin_ConcatSecondEnumerator>(ConcatEnumerable<TJoin_RightItem, TJoin_ConcatFirstEnumerable, TJoin_ConcatFirstEnumerator, TJoin_ConcatSecondEnumerable, TJoin_ConcatSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_ConcatFirstEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ConcatFirstEnumerator>
            where TJoin_ConcatFirstEnumerator : struct, IStructEnumerator<TJoin_RightItem>
            where TJoin_ConcatSecondEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_ConcatSecondEnumerator>
            where TJoin_ConcatSecondEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate>(SelectWhereEnumerable<TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInnerItem>
            where TJoin_SelectProjection : struct, IStructProjection<TJoin_RightItem, TJoin_SelectInnerItem>
            where TJoin_SelectPredicate : struct, IStructPredicate<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupByCollectionSpecificEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_RightItem, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection>(WhereSelectEnumerable<TJoin_RightItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_WhereInnerItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_WhereInnerItem>
            where TJoin_WherePredicate : struct, IStructPredicate<TJoin_WhereInnerItem>
            where TJoin_WhereProjection : struct, IStructProjection<TJoin_RightItem, TJoin_WhereInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupByCollectionDefaultEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_RightItem, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupByCollectionSpecificEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_RightItem, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate>(SelectWhereEnumerable<TJoin_RightItem, TJoin_SelectInnerItem, TJoin_SelectInnerEnumerable, TJoin_SelectInnerEnumerator, TJoin_SelectProjection, TJoin_SelectPredicate> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectInnerEnumerable : struct, IStructEnumerable<TJoin_SelectInnerItem, TJoin_SelectInnerEnumerator>
            where TJoin_SelectInnerEnumerator : struct, IStructEnumerator<TJoin_SelectInnerItem>
            where TJoin_SelectProjection : struct, IStructProjection<TJoin_RightItem, TJoin_SelectInnerItem>
            where TJoin_SelectPredicate : struct, IStructPredicate<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyIndexedBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyIndexedEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_RightItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_RightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection>(WhereSelectEnumerable<TJoin_RightItem, TJoin_WhereInnerItem, TJoin_WhereInnerEnumerable, TJoin_WhereInnerEnumerator, TJoin_WherePredicate, TJoin_WhereProjection> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_WhereInnerEnumerable : struct, IStructEnumerable<TJoin_WhereInnerItem, TJoin_WhereInnerEnumerator>
            where TJoin_WhereInnerEnumerator : struct, IStructEnumerator<TJoin_WhereInnerItem>
            where TJoin_WherePredicate : struct, IStructPredicate<TJoin_WhereInnerItem>
            where TJoin_WhereProjection : struct, IStructProjection<TJoin_RightItem, TJoin_WhereInnerItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator>(GroupByCollectionDefaultEnumerable<TJoin_GroupByInItem, TJoin_GroupByKey, TJoin_GroupByElement, TJoin_RightItem, TJoin_GroupByEnumerable, TJoin_GroupByEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_GroupByEnumerable : struct, IStructEnumerable<TJoin_GroupByInItem, TJoin_GroupByEnumerator>
            where TJoin_GroupByEnumerator : struct, IStructEnumerator<TJoin_GroupByInItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_SelectManyCollectionItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_SelectManyCollectionItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator>(ZipEnumerable<TJoin_RightItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_ZipFirstEnumerable : struct, IStructEnumerable<TJoin_ZipFirstItem, TJoin_ZipFirstEnumerator>
            where TJoin_ZipFirstEnumerator : struct, IStructEnumerator<TJoin_ZipFirstItem>
            where TJoin_ZipSecondEnumerable : struct, IStructEnumerable<TJoin_ZipSecondItem, TJoin_ZipSecondEnumerator>
            where TJoin_ZipSecondEnumerator : struct, IStructEnumerator<TJoin_ZipSecondItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_SelectManyCollectionItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyInItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerable, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerable : struct, IStructEnumerable<TJoin_SelectManyCollectionItem, TJoin_SelectManyProjectedEnumerator>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionIndexedBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator>(ZipEnumerable<TJoin_RightItem, TJoin_ZipFirstItem, TJoin_ZipSecondItem, TJoin_ZipFirstEnumerable, TJoin_ZipFirstEnumerator, TJoin_ZipSecondEnumerable, TJoin_ZipSecondEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_ZipFirstEnumerable : struct, IStructEnumerable<TJoin_ZipFirstItem, TJoin_ZipFirstEnumerator>
            where TJoin_ZipFirstEnumerator : struct, IStructEnumerator<TJoin_ZipFirstItem>
            where TJoin_ZipSecondEnumerable : struct, IStructEnumerable<TJoin_ZipSecondItem, TJoin_ZipSecondEnumerator>
            where TJoin_ZipSecondEnumerator : struct, IStructEnumerator<TJoin_ZipSecondItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyInItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator>(SelectManyCollectionBridgeEnumerable<TJoin_SelectManyInItem, TJoin_RightItem, TJoin_SelectManyCollectionItem, TJoin_SelectManyBridgeType, TJoin_SelectManyInnerEnumerable, TJoin_SelectManyInnerEnumerator, TJoin_SelectManyProjectedEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_SelectManyBridgeType : class
            where TJoin_SelectManyInnerEnumerable : struct, IStructEnumerable<TJoin_SelectManyInItem, TJoin_SelectManyInnerEnumerator>
            where TJoin_SelectManyInnerEnumerator : struct, IStructEnumerator<TJoin_SelectManyInItem>
            where TJoin_SelectManyProjectedEnumerator : struct, IStructEnumerator<TJoin_SelectManyCollectionItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerator>
            where TJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TJoin_GroupJoinLeftItem>
            where TJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerator>
            where TJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator>(JoinSpecificEnumerable<TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_JoinLeftEnumerable : struct, IStructEnumerable<TJoin_JoinLeftItem, TJoin_JoinLeftEnumerator>
            where TJoin_JoinLeftEnumerator : struct, IStructEnumerator<TJoin_JoinLeftItem>
            where TJoin_JoinRightEnumerable : struct, IStructEnumerable<TJoin_JoinRightItem, TJoin_JoinRightEnumerator>
            where TJoin_JoinRightEnumerator : struct, IStructEnumerator<TJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator>(JoinDefaultEnumerable<TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_JoinLeftEnumerable : struct, IStructEnumerable<TJoin_JoinLeftItem, TJoin_JoinLeftEnumerator>
            where TJoin_JoinLeftEnumerator : struct, IStructEnumerator<TJoin_JoinLeftItem>
            where TJoin_JoinRightEnumerable : struct, IStructEnumerable<TJoin_JoinRightItem, TJoin_JoinRightEnumerator>
            where TJoin_JoinRightEnumerator : struct, IStructEnumerator<TJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
            where TJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerator>
            where TJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TJoin_GroupJoinLeftItem>
            where TJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerator>
            where TJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator>(JoinSpecificEnumerable<TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_JoinLeftEnumerable : struct, IStructEnumerable<TJoin_JoinLeftItem, TJoin_JoinLeftEnumerator>
            where TJoin_JoinLeftEnumerator : struct, IStructEnumerator<TJoin_JoinLeftItem>
            where TJoin_JoinRightEnumerable : struct, IStructEnumerable<TJoin_JoinRightItem, TJoin_JoinRightEnumerator>
            where TJoin_JoinRightEnumerator : struct, IStructEnumerator<TJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator>(JoinDefaultEnumerable<TJoin_RightItem, TJoin_JoinKeyItem, TJoin_JoinLeftItem, TJoin_JoinLeftEnumerable, TJoin_JoinLeftEnumerator, TJoin_JoinRightItem, TJoin_JoinRightEnumerable, TJoin_JoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_JoinLeftEnumerable : struct, IStructEnumerable<TJoin_JoinLeftItem, TJoin_JoinLeftEnumerator>
            where TJoin_JoinLeftEnumerator : struct, IStructEnumerator<TJoin_JoinLeftItem>
            where TJoin_JoinRightEnumerable : struct, IStructEnumerable<TJoin_JoinRightItem, TJoin_JoinRightEnumerator>
            where TJoin_JoinRightEnumerator : struct, IStructEnumerator<TJoin_JoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator>(GroupJoinSpecificEnumerable<TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerator>
            where TJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TJoin_GroupJoinLeftItem>
            where TJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerator>
            where TJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator>(GroupJoinDefaultEnumerable<TJoin_RightItem, TJoin_GroupJoinKeyItem, TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerable, TJoin_GroupJoinLeftEnumerator, TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerable, TJoin_GroupJoinRightEnumerator> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
            where TJoin_GroupJoinLeftEnumerable : struct, IStructEnumerable<TJoin_GroupJoinLeftItem, TJoin_GroupJoinLeftEnumerator>
            where TJoin_GroupJoinLeftEnumerator : struct, IStructEnumerator<TJoin_GroupJoinLeftItem>
            where TJoin_GroupJoinRightEnumerable : struct, IStructEnumerable<TJoin_GroupJoinRightItem, TJoin_GroupJoinRightEnumerator>
            where TJoin_GroupJoinRightEnumerator : struct, IStructEnumerator<TJoin_GroupJoinRightItem>
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemDefaultEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemDefaultEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemSpecificEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }

        public EmptyEnumerable<TJoin_OutItem> Join<TJoin_OutItem, TJoin_KeyItem, TJoin_RightItem>(OneItemSpecificEnumerable<TJoin_RightItem> inner, Func<TItem, TJoin_KeyItem> outerKeySelector, Func<TJoin_RightItem, TJoin_KeyItem> innerKeySelector, Func<TItem, TJoin_RightItem, TJoin_OutItem> resultSelector, IEqualityComparer<TJoin_KeyItem> comparer)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "outer");
            if (inner.IsDefaultValue()) throw new ArgumentException("Argument uninitialized", nameof(inner));
            if (outerKeySelector == null) throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null) throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return EmptyCache<TJoin_OutItem>.Empty;
        }
    }
}
