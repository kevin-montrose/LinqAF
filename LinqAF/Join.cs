using System;
using System.Collections.Generic;

namespace LinqAF
{
    public struct JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>:
        IStructEnumerator<TOutItem>
        where TLeftEnumerator: struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator: struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        Dictionary<TKeyItem, List<TRightItem>> InnerLookup;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;

        TLeftItem CurrentKey;
        ListEnumerator<TRightItem> NeedsYield;

        internal JoinDefaultEnumerator(
            ref TLeftEnumerator left, 
            ref TRightEnumerator right, 
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = null;
            Current = default(TOutItem);
            NeedsYield = default(ListEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);

        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                InnerLookup == null &&
                NeedsYield.IsDefaultValue() &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public bool MoveNext()
        {
            if(InnerLookup == null)
            {
                InnerLookup = new Dictionary<TKeyItem, List<TRightItem>>(EqualityComparer<TKeyItem>.Default);
                while (Right.MoveNext())
                {
                    var cur = Right.Current;
                    var curKey = RightKeySelector(cur);

                    if (curKey == null) continue;

                    List<TRightItem> forKey;
                    if(!InnerLookup.TryGetValue(curKey, out forKey))
                    {
                        InnerLookup[curKey] = forKey = new List<TRightItem>();
                    }
                    forKey.Add(cur);
                }
            }

            continueYielding:
            if (!NeedsYield.IsDefaultValue())
            {
                if (NeedsYield.MoveNext())
                {
                    var rightCur = NeedsYield.Current;
                    Current = ResultSelector(CurrentKey, rightCur);
                    return true;
                }

                NeedsYield = default(ListEnumerator<TRightItem>);
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null) continue;

                List<TRightItem> forKey;
                if(InnerLookup.TryGetValue(curKey, out forKey))
                {
                    NeedsYield = new ListEnumerator<TRightItem>(forKey);
                    CurrentKey = cur;
                    goto continueYielding;
                }
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = null;
            NeedsYield = default(ListEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = null;
            NeedsYield = default(ListEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }
    }

    public partial struct JoinDefaultEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator>:
        IStructEnumerable<TOutItem, JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable: struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator: struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable: struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator: struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;
        internal JoinDefaultEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector)
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();
            return new JoinDefaultEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector);
        }
    }

    public partial struct JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> :
        IStructEnumerator<TOutItem>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        public TOutItem Current { get; private set; }

        TLeftEnumerator Left;
        TRightEnumerator Right;
        Dictionary<TKeyItem, List<TRightItem>> InnerLookup;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;

        TLeftItem CurrentKey;
        ListEnumerator<TRightItem> NeedsYield;

        internal JoinSpecificEnumerator(
            ref TLeftEnumerator left,
            ref TRightEnumerator right,
            Func<TLeftItem, TKeyItem> leftKeySelector,
            Func<TRightItem, TKeyItem> rightKeySelector,
            Func<TLeftItem, TRightItem, TOutItem> resultSelector,
            IEqualityComparer<TKeyItem> comparer
        )
        {
            Left = left;
            Right = right;
            LeftKeySelector = leftKeySelector;
            RightKeySelector = rightKeySelector;
            ResultSelector = resultSelector;
            InnerLookup = null;
            Current = default(TOutItem);
            NeedsYield = default(ListEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
            Comparer = comparer;

        }

        public bool IsDefaultValue()
        {
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                InnerLookup == null &&
                Comparer == null &&
                NeedsYield.IsDefaultValue() &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public bool MoveNext()
        {
            if (InnerLookup == null)
            {
                InnerLookup = new Dictionary<TKeyItem, List<TRightItem>>(Comparer);
                while (Right.MoveNext())
                {
                    var cur = Right.Current;
                    var curKey = RightKeySelector(cur);

                    if (curKey == null) continue;

                    List<TRightItem> forKey;
                    if (!InnerLookup.TryGetValue(curKey, out forKey))
                    {
                        InnerLookup[curKey] = forKey = new List<TRightItem>();
                    }
                    forKey.Add(cur);
                }
            }

            continueYielding:
            if (!NeedsYield.IsDefaultValue())
            {
                if (NeedsYield.MoveNext())
                {
                    var rightCur = NeedsYield.Current;
                    Current = ResultSelector(CurrentKey, rightCur);
                    return true;
                }

                NeedsYield = default(ListEnumerator<TRightItem>);
            }

            while (Left.MoveNext())
            {
                var cur = Left.Current;
                var curKey = LeftKeySelector(cur);

                if (curKey == null) continue;

                List<TRightItem> forKey;
                if (InnerLookup.TryGetValue(curKey, out forKey))
                {
                    NeedsYield = new ListEnumerator<TRightItem>(forKey);
                    CurrentKey = cur;
                    goto continueYielding;
                }
            }

            return false;
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
            InnerLookup = null;
            NeedsYield = default(ListEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }

        public void Dispose()
        {
            Left.Dispose();
            Right.Dispose();
            InnerLookup = null;
            NeedsYield = default(ListEnumerator<TRightItem>);
            CurrentKey = default(TLeftItem);
        }
    }

    public partial struct JoinSpecificEnumerable<TOutItem, TKeyItem, TLeftItem, TLeftEnumerable, TLeftEnumerator, TRightItem, TRightEnumerable, TRightEnumerator> :
        IStructEnumerable<TOutItem, JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>>
        where TLeftEnumerable : struct, IStructEnumerable<TLeftItem, TLeftEnumerator>
        where TLeftEnumerator : struct, IStructEnumerator<TLeftItem>
        where TRightEnumerable : struct, IStructEnumerable<TRightItem, TRightEnumerator>
        where TRightEnumerator : struct, IStructEnumerator<TRightItem>
    {
        TLeftEnumerable Left;
        TRightEnumerable Right;
        Func<TLeftItem, TKeyItem> LeftKeySelector;
        Func<TRightItem, TKeyItem> RightKeySelector;
        Func<TLeftItem, TRightItem, TOutItem> ResultSelector;
        IEqualityComparer<TKeyItem> Comparer;
        internal JoinSpecificEnumerable(ref TLeftEnumerable left, ref TRightEnumerable right, Func<TLeftItem, TKeyItem> leftKeySelector, Func<TRightItem, TKeyItem> rightKeySelector, Func<TLeftItem, TRightItem, TOutItem> resultSelector, IEqualityComparer<TKeyItem> comparer)
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
            return
                LeftKeySelector == null &&
                RightKeySelector == null &&
                ResultSelector == null &&
                Comparer == null &&
                Left.IsDefaultValue() &&
                Right.IsDefaultValue();
        }

        public JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator> GetEnumerator()
        {
            var leftE = Left.GetEnumerator();
            var rightE = Right.GetEnumerator();
            return new JoinSpecificEnumerator<TOutItem, TKeyItem, TLeftItem, TLeftEnumerator, TRightItem, TRightEnumerator>(ref leftE, ref rightE, LeftKeySelector, RightKeySelector, ResultSelector, Comparer);
        }
    }
}
