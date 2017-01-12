namespace LinqAF.Impl
{
    struct OrderBySortItem<TItem, TKey>
    {
        public TItem Item;
        public TKey Key;
        public int Index;
    }
}
