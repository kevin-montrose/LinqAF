namespace LinqAF.Impl
{
    internal interface IElementAt<TItem>
    {
        TItem ElementAt(int index);
        TItem ElementAtOrDefault(int index);
    }
}
