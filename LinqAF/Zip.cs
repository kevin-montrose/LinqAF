using System;

namespace LinqAF
{
    public struct ZipEnumerator<TOutItem, TFirstItem, TSecondItem, TFirstEnumerator, TSecondEnumerator>:
        IStructEnumerator<TOutItem>
        where TFirstEnumerator: struct, IStructEnumerator<TFirstItem>
        where TSecondEnumerator: struct, IStructEnumerator<TSecondItem>
    {
        public TOutItem Current { get; private set; }

        TFirstEnumerator FirstEnumerator;
        TSecondEnumerator SecondEnumerator;
        Func<TFirstItem, TSecondItem, TOutItem> ResultSelector;
        
        internal ZipEnumerator(ref TFirstEnumerator first, ref TSecondEnumerator second, Func<TFirstItem, TSecondItem, TOutItem> resultSelector)
        {
            FirstEnumerator = first;
            SecondEnumerator = second;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
        }

        public bool IsDefaultValue()
        {
            return
                ResultSelector == null &&
                FirstEnumerator.IsDefaultValue() &&
                SecondEnumerator.IsDefaultValue();
        }

        public bool MoveNext()
        {
            if (!FirstEnumerator.MoveNext()) return false;
            if (!SecondEnumerator.MoveNext()) return false;

            var first = FirstEnumerator.Current;
            var second = SecondEnumerator.Current;
            
            Current = ResultSelector(first, second);

            return true;
        }

        public void Reset()
        {
            FirstEnumerator.Reset();
            SecondEnumerator.Reset();
            Current = default(TOutItem);
        }

        public void Dispose()
        {
            FirstEnumerator.Dispose();
            SecondEnumerator.Dispose();
        }
    }

    public partial struct ZipEnumerable<TOutItem, TFirstItem, TSecondItem, TFirstEnumerable, TFirstEnumerator, TSecondEnumerable, TSecondEnumerator> :
        IStructEnumerable<TOutItem, ZipEnumerator<TOutItem, TFirstItem, TSecondItem, TFirstEnumerator, TSecondEnumerator>>
        where TFirstEnumerable: struct, IStructEnumerable<TFirstItem, TFirstEnumerator>
        where TFirstEnumerator: struct, IStructEnumerator<TFirstItem>
        where TSecondEnumerable: struct, IStructEnumerable<TSecondItem, TSecondEnumerator>
        where TSecondEnumerator: struct, IStructEnumerator<TSecondItem>
    {
        TFirstEnumerable FirstEnumerable;
        TSecondEnumerable SecondEnumerable;
        Func<TFirstItem, TSecondItem, TOutItem> ResultSelector;
        internal ZipEnumerable(ref TFirstEnumerable first, ref TSecondEnumerable second, Func<TFirstItem, TSecondItem, TOutItem> resultSelector)
        {
            FirstEnumerable = first;
            SecondEnumerable = second;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return
                ResultSelector == null &&
                FirstEnumerable.IsDefaultValue() &&
                SecondEnumerable.IsDefaultValue();
        }

        public ZipEnumerator<TOutItem, TFirstItem, TSecondItem, TFirstEnumerator, TSecondEnumerator> GetEnumerator()
        {
            var first = FirstEnumerable.GetEnumerator();
            var second = SecondEnumerable.GetEnumerator();
            return new ZipEnumerator<TOutItem, TFirstItem, TSecondItem, TFirstEnumerator, TSecondEnumerator>(ref first, ref second, ResultSelector);
        }
    }
}
