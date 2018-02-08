namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Append<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, TItem element)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return AppendImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, element);
        }

        public static AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> AppendImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, TItem element)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            return new AppendEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, element);
        }
    }
}
