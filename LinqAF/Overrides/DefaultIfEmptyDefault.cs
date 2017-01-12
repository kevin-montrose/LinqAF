using System;

namespace LinqAF
{
    partial struct DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>
    {
        public bool Any()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return true;
        }

        public DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty()
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }

        public DefaultIfEmptyDefaultEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> DefaultIfEmpty(TItem item)
        {
            if (IsDefaultValue()) throw new ArgumentException("Argument uninitialized", "source");

            return this;
        }
    }
}
