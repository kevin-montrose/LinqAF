using LinqAF.Config;
using System;

namespace LinqAF.Impl
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    internal struct StructStack<T>: IDisposable
    {
        const int DEFAULT_SIZE = 4;

        public int Count => InnerCount;

        int InnerCount;
        T[] Buffer;

        public bool IsDefaultValue() => Buffer == null;

        public void Push(T val)
        {
            if(Buffer == null)
            {
                Buffer = Allocator.Current.GetArray<T>(DEFAULT_SIZE);
            }

            if(InnerCount == Buffer.Length)
            {
                Allocator.Current.ResizeArray(ref Buffer, CommonImplementation.NextSize(Buffer.Length));
            }

            Buffer[InnerCount] = val;
            InnerCount++;
        }

        public T Pop()
        {
            if (InnerCount <= 0) throw CommonImplementation.UnexpectedState();

            InnerCount--;

            var ret = Buffer[InnerCount];
            return ret;
        }

        public void Dispose()
        {
            InnerCount = 0;
            Buffer = null;
        }
    }
}
