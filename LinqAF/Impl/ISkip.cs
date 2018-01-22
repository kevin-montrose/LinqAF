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
    }

    interface ISkipSpecialized<TItem, TThisEnumerable, TThisEnumerator, TSkipEnumerable, TSkipEnumerator, TSkipWhileEnumerable, TSkipWhileEnumerator, TSkipWhileIndexedEnumerable, TSkipWhileIndexedEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TItem>
        where TSkipEnumerable : struct, IStructEnumerable<TItem, TSkipEnumerator>
        where TSkipEnumerator : struct, IStructEnumerator<TItem>
        where TSkipWhileEnumerable : struct, IStructEnumerable<TItem, TSkipWhileEnumerator>
        where TSkipWhileEnumerator : struct, IStructEnumerator<TItem>
        where TSkipWhileIndexedEnumerable : struct, IStructEnumerable<TItem, TSkipWhileIndexedEnumerator>
        where TSkipWhileIndexedEnumerator : struct, IStructEnumerator<TItem>
    {
        TSkipEnumerable Skip(int count);

        TSkipWhileEnumerable SkipWhile(Func<TItem, bool> predicate);

        TSkipWhileIndexedEnumerable SkipWhile(Func<TItem, int, bool> predicate);
    }
}
