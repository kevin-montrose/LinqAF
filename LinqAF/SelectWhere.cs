using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectWhereEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection, TPredicate>:
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInnerItem>
        where TProjection: struct, IStructProjection<TOutItem, TInnerItem>
        where TPredicate: struct, IStructPredicate<TOutItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        TProjection Project;
        TPredicate Predicate;

        internal SelectWhereEnumerator(ref TInnerEnumerator inner, ref TProjection project, ref TPredicate predicate)
        {
            Inner = inner;
            Project = project;
            Predicate = predicate;
            Current = default(TOutItem);
        }

        public void Dispose()
        {
            Inner.Dispose();
        }

        public bool IsDefaultValue()
        {
            return Project.IsDefaultValue();
        }

        public bool MoveNext()
        {
            while (Inner.MoveNext())
            {
                var potentialItem = Project.Project(Inner.Current);
                if (Predicate.IsMatch(potentialItem))
                {
                    Current = potentialItem;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TOutItem);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectWhereEnumerable<TOutItem, TInnerItem, TInnerEnumerable, TInnerEnumerator, TProjection, TPredicate>:
        IStructEnumerable<TOutItem, SelectWhereEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection, TPredicate>>
        where TInnerEnumerable: struct, IStructEnumerable<TInnerItem, TInnerEnumerator>
        where TInnerEnumerator: struct, IStructEnumerator<TInnerItem>
        where TProjection: struct, IStructProjection<TOutItem, TInnerItem>
        where TPredicate: struct, IStructPredicate<TOutItem>
    {
        TInnerEnumerable Inner;
        TProjection Project;
        TPredicate Predicate;

        internal SelectWhereEnumerable(ref TInnerEnumerable inner, ref TProjection project, ref TPredicate predicate)
        {
            Inner = inner;
            Project = project;
            Predicate = predicate;
        }

        public bool IsDefaultValue()
        {
            return Project.IsDefaultValue();
        }

        public SelectWhereEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection, TPredicate> GetEnumerator()
        {
            var e = Inner.GetEnumerator();
            return new SelectWhereEnumerator<TOutItem, TInnerItem, TInnerEnumerator, TProjection, TPredicate>(ref e, ref Project, ref Predicate);
        }
    }
}
