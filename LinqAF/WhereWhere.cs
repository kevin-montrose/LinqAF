namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct WhereWhereEnumerator<TItem, TInnerEnumerator, TPredicate>:
        IStructEnumerator<TItem>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
        where TPredicate: struct, IStructPredicate<TItem>
    {
        public TItem Current { get; private set; }

        TInnerEnumerator Inner;
        TPredicate Predicate;

        internal WhereWhereEnumerator(ref TInnerEnumerator inner, ref TPredicate predicate)
        {
            Inner = inner;
            Predicate = predicate;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return Predicate.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            while (Inner.MoveNext())
            {
                var cur = Inner.Current;
                if (Predicate.IsMatch(cur))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct WhereWhereEnumerable<TItem, TInnerEnumerable, TInnerEnumerator, TPredicate>:
        IStructEnumerable<TItem, WhereWhereEnumerator<TItem, TInnerEnumerator, TPredicate>>
        where TInnerEnumerable: struct, IStructEnumerable<TItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TItem>
        where TPredicate: struct, IStructPredicate<TItem>
    {
        TInnerEnumerable Inner;
        TPredicate Predicate;

        internal WhereWhereEnumerable(ref TInnerEnumerable inner, ref TPredicate predicate)
        {
            Inner = inner;
            Predicate = predicate;
        }

        public bool IsDefaultValue()
        {
            return Predicate.IsDefaultValue();
        }

        public WhereWhereEnumerator<TItem, TInnerEnumerator, TPredicate> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new WhereWhereEnumerator<TItem, TInnerEnumerator, TPredicate>(ref inner, ref Predicate);
        }
    }
}
