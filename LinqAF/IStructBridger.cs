namespace LinqAF
{
    public interface IStructBridger<TItem, TBridgeType, TEnumerator>
        where TEnumerator : struct, IStructEnumerator<TItem>
        where TBridgeType : class
    {
        TEnumerator Bridge(TBridgeType enumerable);
    }
}
