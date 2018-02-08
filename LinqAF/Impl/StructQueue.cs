using LinqAF.Config;
using System;
using System.Runtime.InteropServices;

namespace LinqAF.Impl
{
    [StructLayout(LayoutKind.Auto)]
    struct StructQueue<TItem>
    {
        const int DEFAULT_SIZE = 4;

        public int Count { get; private set; }

        TItem[] Buffer;
        int ReadFrom;
        int WriteTo;
        
        public void Initialize(int? size = null)
        {
            Buffer = Allocator.Current.GetArray<TItem>(size ?? DEFAULT_SIZE);
            ReadFrom = 0;
            WriteTo = 0;
            Count = 0;
        }

        public bool IsDefaultValue() => Buffer == null;

        public void Enqueue(TItem item)
        {
            TryGrow();

            EnqueueImpl(item);
        }

        void EnqueueImpl(TItem item)
        {
            Buffer[WriteTo] = item;
            AdvanceWriteTo();
            Count++;
        }

        public TItem Dequeue()
        {
            if (Count == 0) throw CommonImplementation.InvalidOperation("Tried to Dequeue from an empty Queue");

            var ret = Buffer[ReadFrom];

#if DEBUG
            Buffer[ReadFrom] = default(TItem);
#endif

            AdvanceReadFrom();
            Count--;

            return ret;
        }

        public void DequeueAndEnqueue(TItem item)
        {
            Dequeue();

            // skip growth check, we know it doesn't have to since we 
            //   _just_ freed up some space
            EnqueueImpl(item);
        }

        void TryGrow()
        {
            if (Count != Buffer.Length) return;

            var newBuffer = Allocator.Current.GetArray<TItem>(CommonImplementation.NextSize(Buffer.Length));
            
            // ^ = ReadFrom
            // $ = WriteTo
            // Copy(sourceArray, sourceIndex, destArray, destIndex, length)

            // #1
            // _1234__
            //  ^   $
            //  1   5
            // length = 7
            // count  = 4
            //
            // 1234___
            // ^   $
            // 0   4
            // 
            // actions
            // Copy(original, 1, new, 0, 4)

            // #2
            // 34___12
            //   $  ^
            //   2  5
            // length = 7
            // count  = 4
            //
            // 1234___
            // ^   $
            // 0   5
            //
            // actions
            // Copy(original, 5, new, 0, 2)
            // Copy(original, 0, new, 3, 2)


            // #3
            // 21
            //  $
            //  ^
            //  1
            // length = 2
            // count  = 2
            //
            // 12
            // $
            // ^
            //
            // actions
            // Copy(original, 1, new, 0, 1)
            // Copy(original, 0, new, 1, 1)

            // case #1
            if(ReadFrom < WriteTo)
            {
                Array.Copy(Buffer, ReadFrom, newBuffer, 0, Count);
                ReadFrom = 0;
                WriteTo = Count;
                Buffer = newBuffer;
                return;
            }

            var copyFromEnd = Buffer.Length - ReadFrom;
            Array.Copy(Buffer, ReadFrom, newBuffer, 0, copyFromEnd);
            Array.Copy(Buffer, 0, newBuffer, copyFromEnd, ReadFrom);
            ReadFrom = 0;
            WriteTo = Count;
            Buffer = newBuffer;
        }

        void AdvanceWriteTo() => AdvanceWrapAround(ref WriteTo);

        void AdvanceReadFrom() => AdvanceWrapAround(ref ReadFrom);

        void AdvanceWrapAround(ref int i)
        {
            i++;
            if(i == Buffer.Length)
            {
                i = 0;
            }
        }

        public void Dispose()
        {
            Buffer = null;
            ReadFrom = 0;
            WriteTo = 0;
        }
    }
}
