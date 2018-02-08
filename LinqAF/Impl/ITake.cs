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

        TakeLastEnumerable<TItem, TThisEnumerable, TThisEnumerator> TakeLast(int count);
    }
}