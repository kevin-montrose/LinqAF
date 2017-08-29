using System;

namespace LinqAF
{
    public interface IStructPredicate<TItem>
    {
        bool IsMatch(TItem item);
        bool IsDefaultValue();
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SinglePredicate<TItem>: IStructPredicate<TItem>
    {
        Func<TItem, bool> Predicate;

        internal SinglePredicate(Func<TItem, bool> predicate)
        {
            Predicate = predicate;
        }

        public bool IsDefaultValue() => Predicate == null;

        public bool IsMatch(TItem item) => Predicate(item);
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct ChainedPredicate<TItem, THeadPredicate, TTailPredicate>: 
        IStructPredicate<TItem>
        where THeadPredicate: struct, IStructPredicate<TItem>
        where TTailPredicate: struct, IStructPredicate<TItem>
    {
        THeadPredicate FirstPredicate;
        TTailPredicate SecondPredicate;

        internal ChainedPredicate(ref THeadPredicate first, ref TTailPredicate second)
        {
            FirstPredicate = first;
            SecondPredicate = second;
        }

        public bool IsDefaultValue() => FirstPredicate.IsDefaultValue();

        public bool IsMatch(TItem item) => FirstPredicate.IsMatch(item) && SecondPredicate.IsMatch(item);
    }
}
