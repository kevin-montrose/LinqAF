using System;

namespace LinqAF.Impl
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    sealed class DoNotInjectAttribute: Attribute
    {
    }
}
