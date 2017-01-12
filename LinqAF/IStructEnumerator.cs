using System;

namespace LinqAF
{
    public interface IStructEnumerator<out TItem> : IDisposable
    {
        TItem Current { get; }
        bool MoveNext();
        void Reset();
        bool IsDefaultValue();
    }
}
