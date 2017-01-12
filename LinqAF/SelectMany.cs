using System;

namespace LinqAF
{
    public struct SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> : IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridgeType: class
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        Func<TInItem, TBridgeType> Project;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        TProjectedEnumerator CurrentEnumerator;
        internal SelectManyBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, TBridgeType> project, Func<TBridgeType, TProjectedEnumerator> bridge)
        {
            Inner = inner;
            Project = project;
            Current = default(TOutItem);
            Bridge = bridge;
            CurrentEnumerator = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return
                Project == null &&
                CurrentEnumerator.IsDefaultValue() &&
                Inner.IsDefaultValue();
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
                CurrentEnumerator = Bridge(projected);
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
    
    public partial struct SelectManyBridgeEnumerable<TInItem, TOutItem, TBridgeType, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridgeType: class
    {

        TInnerEnumerable Inner;
        Func<TInItem, TBridgeType> Project;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        internal SelectManyBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, TBridgeType> project, Func<TBridgeType, TProjectedEnumerator> bridge)
        {
            Inner = inner;
            Project = project;
            Bridge = bridge;
        }

        public bool IsDefaultValue()
        {
            return
                Project == null &&
                Inner.IsDefaultValue();
        }

        public SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>(ref inner, Project, Bridge);
        }
    }

    public struct SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> : IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridgeType: class
    {
        public TOutItem Current { get; private set; }

        TInnerEnumerator Inner;
        int Index;
        Func<TInItem, int, TBridgeType> Project;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        TProjectedEnumerator CurrentEnumerator;
        internal SelectManyIndexedBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, int, TBridgeType> project, Func<TBridgeType, TProjectedEnumerator> bridge)
        {
            Inner = inner;
            Index = 0;
            Project = project;
            Bridge = bridge;
            Current = default(TOutItem);
            CurrentEnumerator = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return
                Project == null &&
                Index == default(int) &&
                CurrentEnumerator.IsDefaultValue() &&
                Inner.IsDefaultValue();
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
                CurrentEnumerator = Bridge(projected);
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

    public partial struct SelectManyIndexedBridgeEnumerable<TInItem, TOutItem, TBridgeType, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TOutItem>
        where TBridgeType: class
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TBridgeType> Project;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        internal SelectManyIndexedBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, int, TBridgeType> project, Func<TBridgeType, TProjectedEnumerator> bridge)
        {
            Inner = inner;
            Project = project;
            Bridge = bridge;
        }

        public bool IsDefaultValue()
        {
            return
                Project == null &&
                Inner.IsDefaultValue();
        }
        
        public SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyIndexedBridgeEnumerator<TInItem, TOutItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>(ref inner, Project, Bridge);
        }
    }

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
            return
                Project == null &&
                Inner.IsDefaultValue() &&
                CurrentEnumerator.IsDefaultValue();
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
                    throw new InvalidOperationException("Uninitialized enumerable returned by projection");
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
            return
                Project == null &&
                Inner.IsDefaultValue();
        }

        public SelectManyEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, Project);
        }
    }

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
            return
                Project == null &&
                Index == default(int) &&
                Inner.IsDefaultValue() &&
                CurrentEnumerator.IsDefaultValue();
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
                    throw new InvalidOperationException("Uninitialized enumerable returned by projection");
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
            return
                Project == null &&
                Inner.IsDefaultValue();
        }
        
        public SelectManyIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyIndexedEnumerator<TInItem, TOutItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, Project);
        }
    }

    public struct SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridgeType: class
    {
        TInnerEnumerator Inner;
        Func<TInItem, TBridgeType> CollectionSelector;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        public TOutItem Current { get; private set; }
        TInItem CurrentIn;

        TProjectedEnumerator CurrentCollection;

        internal SelectManyCollectionBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, TBridgeType> collectionSelector, Func<TBridgeType, TProjectedEnumerator> bridge, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            Bridge = bridge;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            CurrentCollection = default(TProjectedEnumerator);
        }

        public bool IsDefaultValue()
        {
            return
                CollectionSelector == null &&
                ResultSelector == null &&
                CurrentCollection.IsDefaultValue() &&
                Inner.IsDefaultValue();
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
                CurrentCollection = Bridge(e);
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

    public partial struct SelectManyCollectionBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridgeType: class
    {
        TInnerEnumerable Inner;
        Func<TInItem, TBridgeType> CollectionSelector;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        internal SelectManyCollectionBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, TBridgeType> collectionSelector, Func<TBridgeType, TProjectedEnumerator> bridge, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            Bridge = bridge;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return
                CollectionSelector == null &&
                ResultSelector == null &&
                Inner.IsDefaultValue();
        }

        public SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>(ref inner, CollectionSelector, Bridge, ResultSelector);
        }
    }

    public struct SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerator<TOutItem>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridgeType: class
    {
        TInnerEnumerator Inner;
        Func<TInItem, int, TBridgeType> CollectionSelector;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        public TOutItem Current { get; private set; }

        TInItem CurrentIn;
        TProjectedEnumerator CurrentCollection;
        int CurrentIndex;

        internal SelectManyCollectionIndexedBridgeEnumerator(ref TInnerEnumerator inner, Func<TInItem, int, TBridgeType> collectionSelector, Func<TBridgeType, TProjectedEnumerator> bridge, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            Bridge = bridge;
            ResultSelector = resultSelector;
            Current = default(TOutItem);
            CurrentIn = default(TInItem);
            CurrentCollection = default(TProjectedEnumerator);
            CurrentIndex = 0;
        }

        public bool IsDefaultValue()
        {
            return
                CollectionSelector == null &&
                ResultSelector == null &&
                CurrentCollection.IsDefaultValue() &&
                Inner.IsDefaultValue();
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
                CurrentCollection = Bridge(e);
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

    public partial struct SelectManyCollectionIndexedBridgeEnumerable<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerable, TInnerEnumerator, TProjectedEnumerator> :
        IStructEnumerable<TOutItem, SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>>
        where TInnerEnumerable : struct, IStructEnumerable<TInItem, TInnerEnumerator>
        where TInnerEnumerator : struct, IStructEnumerator<TInItem>
        where TProjectedEnumerator: struct, IStructEnumerator<TCollectionItem>
        where TBridgeType: class
    {
        TInnerEnumerable Inner;
        Func<TInItem, int, TBridgeType> CollectionSelector;
        Func<TBridgeType, TProjectedEnumerator> Bridge;
        Func<TInItem, TCollectionItem, TOutItem> ResultSelector;

        internal SelectManyCollectionIndexedBridgeEnumerable(ref TInnerEnumerable inner, Func<TInItem, int, TBridgeType> collectionSelector, Func<TBridgeType, TProjectedEnumerator> bridge, Func<TInItem, TCollectionItem, TOutItem> resultSelector)
        {
            Inner = inner;
            CollectionSelector = collectionSelector;
            Bridge = bridge;
            ResultSelector = resultSelector;
        }

        public bool IsDefaultValue()
        {
            return
                CollectionSelector == null &&
                ResultSelector == null &&
                Inner.IsDefaultValue();
        }

        public SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionIndexedBridgeEnumerator<TInItem, TOutItem, TCollectionItem, TBridgeType, TInnerEnumerator, TProjectedEnumerator>(ref inner, CollectionSelector, Bridge, ResultSelector);
        }
    }

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
            return
                CollectionSelector == null &&
                ResultSelector == null &&
                Inner.IsDefaultValue();
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
                    throw new InvalidOperationException("Uninitialized enumerable returned by projection");
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
            return
                CollectionSelector == null &&
                ResultSelector == null &
                Inner.IsDefaultValue();
        }

        public SelectManyCollectionEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, CollectionSelector, ResultSelector);
        }
    }

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
            return
                CollectionSelector == null &&
                ResultSelector == null &&
                Inner.IsDefaultValue();
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
                    throw new InvalidOperationException("Uninitialized enumerable returned by projection");
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
            return
                CollectionSelector == null &&
                ResultSelector == null &
                Inner.IsDefaultValue();
        }

        public SelectManyCollectionIndexedEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator> GetEnumerator()
        {
            var inner = Inner.GetEnumerator();
            return new SelectManyCollectionIndexedEnumerator<TInItem, TOutItem, TCollectionItem, TInnerEnumerator, TProjectedEnumerable, TProjectedEnumerator>(ref inner, CollectionSelector, ResultSelector);
        }
    }
}