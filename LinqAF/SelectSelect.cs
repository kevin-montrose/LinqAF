namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection>:
        IStructEnumerator<TOutItem>
        where TInnerEnumerator: struct, IStructEnumerator<TInnerItem>
        where TProjection: struct, IStructProjection<TOutItem, TInnerItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        TProjection Projection;

        internal SelectSelectEnumerator(ref TInnerEnumerator inner, ref TProjection projection)
        {
            Inner = inner;
            Projection = projection;
            Current = default(TOutItem);
        }

        public bool IsDefaultValue()
        {
            return Projection.IsDefaultValue();
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (Inner.MoveNext())
            {
                Current = Projection.Project(Inner.Current);
                return true;
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
    public partial struct SelectSelectEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection>:
        IStructEnumerable<TOutItem, SelectSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection>>
        where TInnerEnumerable: struct, IStructEnumerable<TInnerItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TInnerItem>
        where TProjection: struct, IStructProjection<TOutItem, TInnerItem>
    {
        TInnerEnumerable Inner;
        TProjection Projection;

        internal SelectSelectEnumerable(ref TInnerEnumerable inner, ref TProjection projection)
        {
            Inner = inner;
            Projection = projection;
        }

        public bool IsDefaultValue()
        {
            return Projection.IsDefaultValue();
        }

        public SelectSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new SelectSelectEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection>(ref e, ref Projection);
        }
    }
}
