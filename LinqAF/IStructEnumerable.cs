namespace LinqAF
{
    public interface IStructEnumerable<out TItem, TReturnedEnumerator>
        where TReturnedEnumerator : struct, IStructEnumerator<TItem>
    {
        TReturnedEnumerator GetEnumerator();
        bool IsDefaultValue();
    }
}
