using System.Diagnostics;

namespace LinqAF.Impl
{
    // provides the friendly debug view for most enumerable options
    internal sealed class EnumerableStructDebugView<T, TEnumerable, TEnumerator>
        where TEnumerable: struct, IStructEnumerable<T, TEnumerator>
        where TEnumerator: struct, IStructEnumerator<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TEnumerable Inner;

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var ret = new System.Collections.Generic.List<T>();
                foreach(var item in Inner)
                {
                    ret.Add(item);
                }

                return ret.ToArray();
            }
        }
        
        public EnumerableStructDebugView(TEnumerable enumerable)
        {
            Inner = enumerable;
        }
    }
}
