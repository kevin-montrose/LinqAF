namespace LinqAF.Impl
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    struct OrderBySortItem<TItem, TKey>
    {
        public TItem Item;
        public TKey Key;
        public int Index;
    }
}
