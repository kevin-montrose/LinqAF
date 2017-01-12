using System;
namespace LinqAF.Impl
{
    interface IToArray<TItem>
    {
        TItem[] ToArray();
    }
}
