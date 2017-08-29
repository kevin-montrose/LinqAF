using System;
using System.Collections.Generic;
using LinqAF.Impl;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> :
        IStructEnumerator<TOutItem>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        
        LookupDefaultEnumerable<TKeyItem, TRightItem> InnerLookup;

        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        
        internal GroupJoinDefaultEnumerator(
            ref TLeftEnumerator left,
            ref TRightEnumerator right,
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = default(LookupDefaultEnumerable<TKeyItem, TRightItem>);
            Current = default(TOutItem);

        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public bool MoveNext()
        {
            if (InnerLookup.IsDefaultValue())
            {
                InnerLookup = CommonImplementation.ToLookupImpl(ref Right, RightKeySelector);
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null)
                {
                    Current = ResultSelector(cur, EmptyCache<TKeyItem, TRightItem>.EmptyGrouped);
                    return true;
                }

                var forKey = InnerLookup[curKey];
                
                var asGrouped = new GroupedEnumerable<TKeyItem, TRightItem>(ref forKey);
                Current = ResultSelector(cur, asGrouped);
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = default(LookupDefaultEnumerable<TKeyItem, TRightItem>);
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = default(LookupDefaultEnumerable<TKeyItem, TRightItem>);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct GroupJoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>:
        IStructEnumerable<TOutItem, GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        internal GroupJoinDefaultEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector)
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();

            return new GroupJoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> :
        IStructEnumerator<TOutItem>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        
        LookupSpecificEnumerable<TKeyItem, TRightItem> InnerLookup;

        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;

        internal GroupJoinSpecificEnumerator(
            ref TLeftEnumerator left,
            ref TRightEnumerator right,
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = default(LookupSpecificEnumerable<TKeyItem, TRightItem>);
            Current = default(TOutItem);
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public bool MoveNext()
        {
            if (InnerLookup.IsDefaultValue())
            {
                InnerLookup = CommonImplementation.ToLookupImpl(ref Right, RightKeySelector, Comparer);
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null)
                {
                    var nullGroup = EmptyCache<TKeyItem, TRightItem>.EmptyGrouping;
                    Current = ResultSelector(cur, new GroupedEnumerable<TKeyItem, TRightItem>(ref nullGroup));
                    return true;
                }

                var forKey = InnerLookup[curKey];
                
                var asGrouped = new GroupedEnumerable<TKeyItem, TRightItem>(ref forKey);
                Current = ResultSelector(cur, asGrouped);
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = default(LookupSpecificEnumerable<TKeyItem, TRightItem>);
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = default(LookupSpecificEnumerable<TKeyItem, TRightItem>);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct GroupJoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator> :
        IStructEnumerable<TOutItem, GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;
        internal GroupJoinSpecificEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, GroupedEnumerable<TKeyItem, TRightItem>, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            Comparer = comparer;
        }

        public bool IsDefaultValue()
        {
            return LeftKeySelector == null;
        }

        public GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();

            return new GroupJoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector, Comparer);
        }
    }
}
