using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct WhereSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TPredicate, TProjection>:
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInnerItem>
        where TPredicate: struct, IStructPredicate<TInnerItem>
        where TProjection: struct, IStructProjection<TOutItem, TInnerItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        TPredicate Predicate;
        TProjection Projection;

        internal WhereSelectEnumerator(ref TInnerEnumerator inner, ref TPredicate predicate, ref TProjection projection)
        {
            Inner = inner;
            Predicate = predicate;
            Projection = projection;
            Current = default(TOutItem);
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
                    Current = Projection.Project(cur);
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TOutItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct WhereSelectEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TPredicate, TProjection>:
        IStructEnumerable<TOutItem, WhereSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TPredicate, TProjection>>
        where TInnerEnumerable: struct, IStructEnumerable<TInnerItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TInnerItem>
        where TPredicate: struct, IStructPredicate<TInnerItem>
        where TProjection: struct, IStructProjection<TOutItem, TInnerItem>
    {
        TInnerEnumerable Inner;
        TPredicate Predicate;
        TProjection Projection;

        internal WhereSelectEnumerable(ref TInnerEnumerable inner, ref TPredicate predicate, ref TProjection projection)
        {
            Inner = inner;
            Predicate = predicate;
            Projection = projection;
        }

        public bool IsDefaultValue()
        {
            return Predicate.IsDefaultValue();
        }

        public WhereSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TPredicate, TProjection> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new WhereSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TPredicate, TProjection>(ref inner, ref Predicate, ref Projection);
        }
    }
}
