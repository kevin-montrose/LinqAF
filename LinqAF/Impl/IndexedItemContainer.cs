using LinqAF.Config;
using System.Runtime.InteropServices;

namespace LinqAF.Impl
{
    // a container class for tracking actual values
    //
    // also allows for multiple collections to share the same
    //   array of items, reducing allocations
    [StructLayout(LayoutKind.Auto)]
    struct IndexedItemContainer<TItem> : System.IDisposable
    {
        const int DEFAULT_SIZE = 8;

        internal int UsedItems;
        internal TItem[] Items;

        internal IndexedItemContainer(int used, TItem[] items)
        {
            UsedItems = used;
            Items = items;
        }

        public void Initialize()
        {
            UsedItems = 0;
            Items = Allocator.Current.GetArray<TItem>(DEFAULT_SIZE);
        }

        public int PlaceIn(TItem item)
        {
            var ret = UsedItems;
            if (ret == Items.Length)
            {
                Allocator.Current.ResizeArray(ref Items, CommonImplementation.NextSize(ret));
            }

            Items[ret] = item;
            UsedItems++;

            return ret;
        }

        public void Dispose()
        {
            UsedItems = 0;
            Items = null;
        }

        public void Reset()
        {
            UsedItems = 0;
            Items = null;
        }
    }
}
