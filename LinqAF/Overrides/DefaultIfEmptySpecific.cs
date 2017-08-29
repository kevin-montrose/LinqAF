using LinqAF.Impl;
using System;

namespace LinqAF
{
    partial struct DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public bool Any()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return true;
        }

        public DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }

        public DefaultIfEmptySpecificEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw CommonImplementation.Uninitialized("source");

            return this;
        }
    }
}