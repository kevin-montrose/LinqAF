namespace LinqAF.Impl
{
    internal interface IBoxedEnumerator<TItem>
    {
        TItem Current { get; }
        bool MoveNext();
        void Reset();
        void Dispose();
        bool IsDefaultValue();
    }

    internal interface IBoxedEnumerable<TItem>
    {
        IBoxedEnumerator<TItem> GetEnumerator();
        bool IsDefaultValue();
    }
}
