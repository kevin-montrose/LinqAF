using System;

namespace LinqAF
{
    public interface IStructProjection<TOutItem, TInItem>
    {
        TOutItem Project(TInItem item);
        bool IsDefaultValue();
    }
    
    public struct SingleProjection<TOutItem, TInItem> : IStructProjection<TOutItem, TInItem>
    {
        Func<TInItem, TOutItem> Projection;
        internal SingleProjection(Func<TInItem, TOutItem> projection)
        {
            Projection = projection;
        }

        public bool IsDefaultValue() => Projection == null;

        public TOutItem Project(TInItem item) => Projection(item);
    }

    public struct ChainedProjection<TOutItem, TInItem, TMiddleItem, TRightProjection, TLeftProjection>: 
        IStructProjection<TOutItem, TInItem>
        where TLeftProjection : struct, IStructProjection<TMiddleItem, TInItem>
        where TRightProjection: struct, IStructProjection<TOutItem, TMiddleItem>
    {
        TRightProjection Right;
        TLeftProjection Left;
        internal ChainedProjection(ref TLeftProjection left, ref TRightProjection right)
        {
            Left = left;
            Right = right;
        }

        public TOutItem Project(TInItem item) => Right.Project(Left.Project(item));

        public bool IsDefaultValue() => Right.IsDefaultValue() || Left.IsDefaultValue();
    }
}
