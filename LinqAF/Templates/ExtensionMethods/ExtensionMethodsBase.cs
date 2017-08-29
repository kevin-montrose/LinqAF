using LinqAF.Impl;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqAF
{
    abstract class ExtensionMethodsBase
    {
        /// <summary>
        /// Represents _all_ Enumerables, including the ones in System.Collections.Generic
        /// </summary>
        public struct PlaceholderEnumerable<TItem> : IStructEnumerable<TItem, PlaceholderEnumerator<TItem>>
        {
            public PlaceholderEnumerator<TItem> GetEnumerator() { throw CommonImplementation.NotImplemented(); }
            public bool IsDefaultValue() { throw CommonImplementation.NotImplemented(); }
        }
        public struct PlaceholderEnumerator<TItem> : IStructEnumerator<TItem>
        {
            public TItem Current { get { throw CommonImplementation.NotImplemented(); } }
            public void Dispose() { throw CommonImplementation.NotImplemented(); }
            public bool IsDefaultValue() { throw CommonImplementation.NotImplemented(); }
            public bool MoveNext() { throw CommonImplementation.NotImplemented(); }
            public void Reset() { throw CommonImplementation.NotImplemented(); }
        }

        // just so BuiltInEnumerable can be cast to IEnumerable<TItem>
        public class FakeEnumerable<TItem> : System.Collections.Generic.IEnumerable<TItem>
        {
            public IEnumerator<TItem> GetEnumerator()
            {
                throw CommonImplementation.NotImplemented();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw CommonImplementation.NotImplemented();
            }
        }

        /// <summary>
        /// Represents just the built in (System.Collections.Generic) Enumerables.
        /// </summary>
        public struct BuiltInEnumerable<TItem>: IStructEnumerable<TItem, BuiltInEnumerator<TItem>>
        {
            public BuiltInEnumerator<TItem> GetEnumerator() { throw CommonImplementation.NotImplemented(); }
            public bool IsDefaultValue() { throw CommonImplementation.NotImplemented(); }

            public static implicit operator FakeEnumerable<TItem>(BuiltInEnumerable<TItem> self)
            {
                throw CommonImplementation.NotImplemented();
            }
        }
        public struct BuiltInEnumerator<TItem>: IStructEnumerator<TItem>
        {
            public TItem Current { get { throw CommonImplementation.NotImplemented(); } }
            public void Dispose() { throw CommonImplementation.NotImplemented(); }
            public bool IsDefaultValue() { throw CommonImplementation.NotImplemented(); }
            public bool MoveNext() { throw CommonImplementation.NotImplemented(); }
            public void Reset() { throw CommonImplementation.NotImplemented(); }
        }

        /// <summary>
        /// Represent just the built in (System.Collections.Generic) Enumerables, but with some special juju.
        /// </summary>
        public struct ConstrainedBuiltInEnumerable<TItem> : IStructEnumerable<TItem, ConstrainedBuiltInEnumerator<TItem>>
        {
            public ConstrainedBuiltInEnumerator<TItem> GetEnumerator() { throw CommonImplementation.NotImplemented(); }
            public bool IsDefaultValue() { throw CommonImplementation.NotImplemented(); }
        }
        public struct ConstrainedBuiltInEnumerator<TItem> : IStructEnumerator<TItem>
        {
            public TItem Current { get { throw CommonImplementation.NotImplemented(); } }
            public void Dispose() { throw CommonImplementation.NotImplemented(); }
            public bool IsDefaultValue() { throw CommonImplementation.NotImplemented(); }
            public bool MoveNext() { throw CommonImplementation.NotImplemented(); }
            public void Reset() { throw CommonImplementation.NotImplemented(); }
        }

        protected abstract dynamic RefParam<TItem>(PlaceholderEnumerable<TItem> source);
        protected abstract dynamic RefLocal<TItem>(BuiltInEnumerable<TItem> source);
        protected abstract dynamic Bridge<TItem>(BuiltInEnumerable<TItem> arg, string argName);
        protected abstract dynamic Bridge<Titem>(ConstrainedBuiltInEnumerable<Titem> arg, string argName);
    }
}
