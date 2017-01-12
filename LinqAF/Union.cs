using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TItem>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        bool FirstFinished;
        TSecondEnumerator SecondInner;
        HashSet<TItem> AlreadyYielded;
        internal UnionDefaultEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second)
        {
            FirstInner = first;
            FirstFinished = false;
            SecondInner = second;
            AlreadyYielded = null;
            Current = default(TItem);
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            AlreadyYielded = null;
        }

        public bool MoveNext()
        {
            var alreadyYielded = AlreadyYielded ?? (AlreadyYielded = new HashSet<TItem>(EqualityComparer<TItem>.Default));

            if (!FirstFinished)
            {
                while (FirstInner.MoveNext())
                {
                    var cur = FirstInner.Current;

                    if (alreadyYielded.Add(cur))
                    {
                        Current = cur;
                        return true;
                    }
                }

                FirstFinished = true;
            }

            while (SecondInner.MoveNext())
            {
                var cur = SecondInner.Current;

                if (alreadyYielded.Add(cur))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            FirstInner.Reset();
            SecondInner.Reset();

            FirstFinished = false;
            AlreadyYielded = null;
        }
    }

    public partial struct UnionDefaultEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator>:
        IStructEnumerable<TItem, UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable: struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator: struct, IStructEnumerator<TItem>
        where TSecondEnumerable: struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator: struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        internal UnionDefaultEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second)
        {
            FirstInner = first;
            SecondInner = second;
        }

        public bool IsDefaultValue()
        {
            return FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new UnionDefaultEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE);
        }
    }

    public struct UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> :
        IStructEnumerator<TItem>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        public TItem Current { get; private set; }

        TFirstEnumerator FirstInner;
        bool FirstFinished;
        TSecondEnumerator SecondInner;
        HashSet<TItem> AlreadyYielded;
        IEqualityComparer<TItem> Comparer;
        internal UnionSpecificEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            FirstFinished = false;
            SecondInner = second;
            AlreadyYielded = null;
            Current = default(TItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return Comparer == null && FirstInner.IsDefaultValue() && SecondInner.IsDefaultValue();
        }

        public void Dispose()
        {
            FirstInner.Dispose();
            SecondInner.Dispose();
            AlreadyYielded = null;
        }

        public bool MoveNext()
        {
            var alreadyYielded = AlreadyYielded ?? (AlreadyYielded = new HashSet<TItem>(Comparer));

            if (!FirstFinished)
            {
                while (FirstInner.MoveNext())
                {
                    var cur = FirstInner.Current;

                    if (alreadyYielded.Add(cur))
                    {
                        Current = cur;
                        return true;
                    }
                }

                FirstFinished = true;
            }

            while (SecondInner.MoveNext())
            {
                var cur = SecondInner.Current;

                if (alreadyYielded.Add(cur))
                {
                    Current = cur;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            FirstInner.Reset();
            SecondInner.Reset();

            FirstFinished = false;
            AlreadyYielded = null;
        }
    }

    public partial struct UnionSpecificEnumerable<TItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> :
        IStructEnumerable<TItem, UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable : struct, IStructEnumerable<TItem, TFirstEnumerator>
        where TFirstEnumerator : struct, IStructEnumerator<TItem>
        where TSecondEnumerable : struct, IStructEnumerable<TItem, TSecondEnumerator>
        where TSecondEnumerator : struct, IStructEnumerator<TItem>
    {
        TFirstEnumerable FirstInner;
        TSecondEnumerable SecondInner;
        IEqualityComparer<TItem> Comparer;
        internal UnionSpecificEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second, IEqualityComparer<TItem> comparer)
        {
            FirstInner = first;
            SecondInner = second;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return 
                Comparer == null &&
                FirstInner.IsDefaultValue() && 
                SecondInner.IsDefaultValue();
        }

        public UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var firstE = FirstInner.GetEnumerator();
            var secondE = SecondInner.GetEnumerator();

            return new UnionSpecificEnumerator<TItem, TFirstEnumerator, TSecondEnumerator>(ref firstE, ref secondE, Comparer);
        }
    }
}
