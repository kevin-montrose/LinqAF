using System;

namespace LinqAF.Impl
{
    interface ITake<TItem, TThisEnumerable, TThisEnumerator>
        where TThisEnumerable: struct, IStructEnumerable<TItem, TThisEnumerator>
        where TThisEnumerator: struct, IStructEnumerator<TItem>
    {
        TakeEnumerable<TItem, TThisEnumerable, TThisEnumerator> Take(int count);

        TakeWhileEnumerable<TItem, TThisEnumerable, TThisEnumerator> TakeWhile(Func<TItem, bool> predicate);

        TakeWhileIndexedEnumerable<TItem, TThisEnumerable, TThisEnumerator> TakeWhile(Func<TItem, int, bool> predicate);
    }

    interface ITakeSpecialized<TItem, TThisEnumerable, TThisEnumerator, TTakeEnumerable, TTakeEnumerator, TTakeWhileEnumerable, TTakeWhileEnumerator, TTakeWhileIndexedEnumerable, TTakeWhileIndexedEnumerator>
        where TThisEnumerable : struct, IStructEnumerable<TItem, TThisEnumerator>
        where TThisEnumerator : struct, IStructEnumerator<TItem>
        where TTakeEnumerable : struct, IStructEnumerable<TItem, TTakeEnumerator>
        where TTakeEnumerator : struct, IStructEnumerator<TItem>
        where TTakeWhileEnumerable : struct, IStructEnumerable<TItem, TTakeWhileEnumerator>
        where TTakeWhileEnumerator : struct, IStructEnumerator<TItem>
        where TTakeWhileIndexedEnumerable : struct, IStructEnumerable<TItem, TTakeWhileIndexedEnumerator>
        where TTakeWhileIndexedEnumerator : struct, IStructEnumerator<TItem>
    {
        TTakeEnumerable Take(int count);

        TTakeWhileEnumerable TakeWhile(Func<TItem, bool> predicate);

        TTakeWhileIndexedEnumerable TakeWhile(Func<TItem, int, bool> predicate);
    }
}