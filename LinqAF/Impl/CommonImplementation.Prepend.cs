namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> Prepend<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, TItem element)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            if (source.IsDefaultValue()) throw CommonImplementation.Uninitialized(nameof(source));

            return PrependImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref source, element);
        }

        public static PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator> PrependImpl<TItem, TInnerEnumerable, TInnerEnumerator>(ref TInnerEnumerable source, TItem element)
            where TInnerEnumerable : struct, IStructEnumerable<TItem, TInnerEnumerator>
            where TInnerEnumerator : struct, IStructEnumerator<TItem>
        {
            return new PrependEnumerable<TItem, TInnerEnumerable, TInnerEnumerator>(element, ref source);
        }
    }
}
