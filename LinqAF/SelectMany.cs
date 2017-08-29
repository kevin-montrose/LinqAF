using LinqAF.Impl;
using System;

namespace LinqAF
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> : IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridger: struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TInItem, TBridgeType> Project;
        TProjectedEnumerator CurrentEnumerator;
        internal SelectManyBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, TBridgeType> project)
        {
            Inner = inner;
            Project = project;
            Current = default(TOutItem);
            CurrentEnumerator = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }

        public void Dispose()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Dispose();
        }

        public bool MoveNext()
        {
            Start:
            if (!CurrentEnumerator.IsDefaultValue())
            {
                if (CurrentEnumerator.MoveNext())
                {
                    Current = CurrentEnumerator.Current;
                    return true;
                }

                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                var toProject = Inner.Current;
                var projected = Project(toProject);
                CurrentEnumerator = default(TBridger).Bridge(projected);
                goto Start;
            }

            return false;
        }

        public void Reset()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridger: struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {

        TInnerEnumerable Inner;
        Func<TInItem, TBridgeType> Project;
        internal SelectManyBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, TBridgeType> project)
        {
            Inner = inner;
            Project = project;
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }

        public SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>(ref inner, Project);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> : IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridger : struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        int Index;
        Func<TInItem, int, TBridgeType> Project;
        TProjectedEnumerator CurrentEnumerator;
        internal SelectManyIndexedBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, int, TBridgeType> project)
        {
            Inner = inner;
            Index = 0;
            Project = project;
            Current = default(TOutItem);
            CurrentEnumerator = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }

        public void Dispose()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Dispose();
        }

        public bool MoveNext()
        {
            Start:
            if (!CurrentEnumerator.IsDefaultValue())
            {
                if (CurrentEnumerator.MoveNext())
                {
                    Current = CurrentEnumerator.Current;
                    return true;
                }

                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                var toProject = Inner.Current;
                var projected = Project(toProject, Index);
                CurrentEnumerator = default(TBridger).Bridge(projected);
                Index++;
                goto Start;
            }

            return false;
        }

        public void Reset()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Reset();
            Index = 0;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyIndexedBridgeEnumerable<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridger: struct, IStructBridger<TOutItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TBridgeType> Project;
        internal SelectManyIndexedBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, int, TBridgeType> project)
        {
            Inner = inner;
            Project = project;
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }
        
        public SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>(ref inner, Project);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TInItem, TProjectedEnumerable> Project;
        TProjectedEnumerator CurrentEnumerator;
        internal SelectManyEnumerator(ref TInnerEnumerator inner, Func<TInItem, TProjectedEnumerable> project)
        {
            Inner = inner;
            Project = project;
            Current = default(TOutItem);
            CurrentEnumerator = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }

        public void Dispose()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Dispose();
        }

        public bool MoveNext()
        {
            Start:
            if (!CurrentEnumerator.IsDefaultValue())
            {
                if (CurrentEnumerator.MoveNext())
                {
                    Current = CurrentEnumerator.Current;
                    return true;
                }

                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                var toProject = Inner.Current;
                var projected = Project(toProject);
                if (projected.IsDefaultValue())
                {
                    throw CommonImplementation.UninitializedProjection();
                }

                CurrentEnumerator = projected.GetEnumerator();
                goto Start;
            }

            return false;
        }

        public void Reset()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Reset();
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
    {
        TInnerEnumerable Inner;
        Func<TInItem, TProjectedEnumerable> Project;
        internal SelectManyEnumerable(ref TInnerEnumerable inner, Func<TInItem, TProjectedEnumerable> project)
        {
            Inner = inner;
            Project = project;
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }

        public SelectManyEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, Project);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        int Index;
        Func<TInItem, int, TProjectedEnumerable> Project;
        TProjectedEnumerator CurrentEnumerator;
        internal SelectManyIndexedEnumerator(ref TInnerEnumerator inner, Func<TInItem, int, TProjectedEnumerable> project)
        {
            Inner = inner;
            Index = 0;
            Project = project;
            Current = default(TOutItem);
            CurrentEnumerator = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }

        public void Dispose()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Dispose();
        }

        public bool MoveNext()
        {
            Start:
            if (!CurrentEnumerator.IsDefaultValue())
            {
                if (CurrentEnumerator.MoveNext())
                {
                    Current = CurrentEnumerator.Current;
                    return true;
                }

                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                var toProject = Inner.Current;
                var projected = Project(toProject, Index);
                if (projected.IsDefaultValue())
                {
                    throw CommonImplementation.UninitializedProjection();
                }

                CurrentEnumerator = projected.GetEnumerator();
                Index++;
                goto Start;
            }

            return false;
        }

        public void Reset()
        {
            if (!CurrentEnumerator.IsDefaultValue())
            {
                CurrentEnumerator.Dispose();
                CurrentEnumerator = default(TProjectedEnumerator);
            }

            Inner.Reset();
            Index = 0;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyIndexedEnumerable<TInItem, TOutItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TOutItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TOutItem>
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TProjectedEnumerable> Project;
        internal SelectManyIndexedEnumerable(ref TInnerEnumerable inner, Func<TInItem, int, TProjectedEnumerable> project)
        {
            Inner = inner;
            Project = project;
        }

        public bool IsDefaultValue()
        {
            return Project == null;
        }
        
        public SelectManyIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, Project);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        TInnerEnumerator Inner;
        Func<TInItem, TBridgeType> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        public TOutItem Current { get; private set; }
        TInItem CurrentIn;

        TProjectedEnumerator CurrentCollection;

        internal SelectManyCollectionBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, TBridgeType> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            CurrentCollection = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public bool MoveNext()
        {
            advanceInCollection:
            if (!CurrentCollection.IsDefaultValue())
            {
                if (CurrentCollection.MoveNext())
                {
                    Current = ResultSelector(CurrentIn, CurrentCollection.Current);
                    return true;
                }

                CurrentCollection.Dispose();
                CurrentCollection = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                CurrentIn = Inner.Current;
                var e = CollectionSelector(CurrentIn);
                CurrentCollection = default(TBridger).Bridge(e);
                goto advanceInCollection;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
            CurrentCollection = default(TProjectedEnumerator);
        }

        public void Dispose()
        {
            Inner.Dispose();
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyCollectionBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        TInnerEnumerable Inner;
        Func<TInItem, TBridgeType> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        internal SelectManyCollectionBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, TBridgeType> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>(ref inner, CollectionSelector, ResultSelector);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        TInnerEnumerator Inner;
        Func<TInItem, int, TBridgeType> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        public TOutItem Current { get; private set; }

        TInItem CurrentIn;
        TProjectedEnumerator CurrentCollection;
        int CurrentIndex;

        internal SelectManyCollectionIndexedBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, int, TBridgeType> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            CurrentCollection = default(TProjectedEnumerator);
            CurrentIndex = 0;
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public bool MoveNext()
        {
            advanceInCollection:
            if (!CurrentCollection.IsDefaultValue())
            {
                if (CurrentCollection.MoveNext())
                {
                    Current = ResultSelector(CurrentIn, CurrentCollection.Current);
                    return true;
                }

                CurrentCollection.Dispose();
                CurrentCollection = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                CurrentIn = Inner.Current;
                var e = CollectionSelector(CurrentIn, CurrentIndex);
                CurrentIndex++;
                CurrentCollection = default(TBridger).Bridge(e);
                goto advanceInCollection;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
            CurrentIndex = 0;
            CurrentCollection = default(TProjectedEnumerator);
        }

        public void Dispose()
        {
            Inner.Dispose();
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyCollectionIndexedBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridger: struct, IStructBridger<TCollectionItem, TBridgeType, TProjectedEnumerator>
        where TBridgeType: class
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TBridgeType> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        internal SelectManyCollectionIndexedBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, int, TBridgeType> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TBridger, TInnerEnumerator, TProjectedEnumerator>(ref inner, CollectionSelector, ResultSelector);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyCollectionEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TInItem, TProjectedEnumerable> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        TInItem CurrentIn;
        TProjectedEnumerator CurrentCollection;

        internal SelectManyCollectionEnumerator(ref TInnerEnumerator inner, Func<TInItem, TProjectedEnumerable> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            CurrentCollection = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public bool MoveNext()
        {
            advanceInCollection:
            if (!CurrentCollection.IsDefaultValue())
            {
                if (CurrentCollection.MoveNext())
                {
                    Current = ResultSelector(CurrentIn, CurrentCollection.Current);
                    return true;
                }

                CurrentCollection.Dispose();
                CurrentCollection = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                CurrentIn = Inner.Current;
                var e = CollectionSelector(CurrentIn);
                if (e.IsDefaultValue())
                {
                    throw CommonImplementation.UninitializedProjection();
                }

                CurrentCollection = e.GetEnumerator();
                goto advanceInCollection;
            }

            return false;
        }

        public void Reset()
        {
            Inner.Reset();
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
            CurrentCollection = default(TProjectedEnumerator);
        }

        public void Dispose()
        {
            Inner.Dispose();
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyCollectionEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyCollectionEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
    {
        TInnerEnumerable Inner;
        Func<TInItem, TProjectedEnumerable> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        internal SelectManyCollectionEnumerable(ref TInnerEnumerable inner, Func<TInItem, TProjectedEnumerable> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public SelectManyCollectionEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, CollectionSelector, ResultSelector);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public struct SelectManyCollectionIndexedEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
        where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TInItem, int, TProjectedEnumerable> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        int CurrentIndex;
        TInItem CurrentIn;
        TProjectedEnumerator CurrentCollection;

        internal SelectManyCollectionIndexedEnumerator(ref TInnerEnumerator inner, Func<TInItem, int, TProjectedEnumerable> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            CurrentCollection = default(TProjectedEnumerator);
            CurrentIndex = 0;
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public bool MoveNext()
        {
            advanceInCollection:
            if (!CurrentCollection.IsDefaultValue())
            {
                if (CurrentCollection.MoveNext())
                {
                    Current = ResultSelector(CurrentIn, CurrentCollection.Current);
                    return true;
                }

                CurrentCollection.Dispose();
                CurrentCollection = default(TProjectedEnumerator);
            }

            if (Inner.MoveNext())
            {
                CurrentIn = Inner.Current;
                var e = CollectionSelector(CurrentIn, CurrentIndex);
                if (e.IsDefaultValue())
                {
                    throw CommonImplementation.UninitializedProjection();
                }

                CurrentIndex++;
                CurrentCollection = e.GetEnumerator();
                goto advanceInCollection;
            }

            return false;
        }

        public void Reset()
        {
            CurrentIndex = 0;
            Inner.Reset();
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
            CurrentCollection = default(TProjectedEnumerator);
        }

        public void Dispose()
        {
            Inner.Dispose();
            if (!CurrentCollection.IsDefaultValue())
            {
                CurrentCollection.Dispose();
            }
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public partial struct SelectManyCollectionIndexedEnumerable<TInItem, TOutItem, TCollectionItem, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyCollectionIndexedEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>>
       where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
       where TInnerEnumerator : struct, IStructEnumerator<TInItem>
       where TProjectedEnumerable : struct, IStructEnumerable<TCollectionItem, TProjectedEnumerator>
       where TProjectedEnumerator : struct, IStructEnumerator<TCollectionItem>
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TProjectedEnumerable> CollectionSelector;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        internal SelectManyCollectionIndexedEnumerable(ref TInnerEnumerable inner, Func<TInItem, int, TProjectedEnumerable> collectionSelector, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return CollectionSelector == null;
        }

        public SelectManyCollectionIndexedEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionIndexedEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, CollectionSelector, ResultSelector);
        }
    }
}