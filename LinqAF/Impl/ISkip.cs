using System;

namespace LinqAF.Impl
{
    interface ISkip<TItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TItem>
    {
        SkipEnumerable<TItem, TThisEnumerable, TThisEnumerator> Skip(int count);

        SkipWhileEnumerable<TItem, TThisEnumerable, TThisEnumerator> SkipWhile(Func<TItem, bool> predicate);

        SkipWhileIndexedEnumerable<TItem, TThisEnumerable, TThisEnumerator> SkipWhile(Func<TItem, int, bool> predicate);

        SkipLastEnumerable<TItem, TThisEnumerable, TThisEnumerator> SkipLast(int count);
    }
}
