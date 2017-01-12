using System.Collections.Generic;

namespace LinqAF
{
    public struct GroupingEnumerator<TElement> :
        IStructEnumerator<TElement>
    {
        public TElement Current { get; private set; }

        ListEnumerator<TElement> Inner;

        internal GroupingEnumerator(ref ListEnumerator<TElement> inner)
        {
            Inner = inner;
            Current = default(TElement);
        }

        public bool IsDefaultValue() => Inner.IsDefaultValue();

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool MoveNext()
        {
            if (Inner.MoveNext())
            {
                Current = Inner.Current;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TElement);
        }
    }

    public partial struct GroupingEnumerable<TKey, TElement> :
        IStructEnumerable<TElement, GroupingEnumerator<TElement>>
    {
        public TKey Key { get; private set; }

        internal List<TElement> Inner;

        internal GroupingEnumerable(TKey key, List<TElement> inner)
        {
            Key = key;
            Inner = inner;
        }

        public bool IsDefaultValue()
        {
            return Inner == null;
        }

        public GroupingEnumerator<TElement> GetEnumerator()
        {
            var i = new ListEnumerator<TElement>(Inner);
            return new GroupingEnumerator<TElement>(ref i);
        }

        public override bool Equals(object obj) => false;
        public override int GetHashCode() => Key.GetHashCode() * 17 + (Inner?.GetHashCode() ?? 0);
    }
}
