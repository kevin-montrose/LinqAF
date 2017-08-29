using System;

namespace LinqAF.Impl
{
    static partial class CommonImplementation
    {
        public static Exception ForbiddenCall(string methodName, string typeName) => new InvalidOperationException($"Called {methodName} on {typeName}");
        public static Exception UnexpectedPath(string methodName) => new InvalidOperationException($"Unexpected path taken through {methodName}");
        public static Exception Uninitialized(string argName) => new ArgumentException("Argument uninitialized", argName);
        public static Exception ArgumentNull(string argName) => new ArgumentNullException(argName);
        public static Exception SequenceEmpty() => new InvalidOperationException("Sequence was empty");
        public static Exception OutOfRange(string argName) => new ArgumentOutOfRangeException(argName);
        public static Exception NoItemsMatched(string argName) => new InvalidOperationException($"No items matched {argName}");
        public static Exception MultipleMatchingElements() => new InvalidOperationException("Sequence contained multiple matching elements");
        public static Exception MultipleElements() => new InvalidOperationException("Sequence contained multiple elements");
        public static Exception UninitializedProjection() => new InvalidOperationException("Uninitialized enumerable returned by projection");
        public static Exception UnexpectedState() => new InvalidOperationException("Entered unexpected state");
        public static Exception NotImplemented() => new NotImplementedException();
        public static Exception InnerUninitialized() => new InvalidOperationException("Inner enumerable is uninitialized");
        public static Exception InvalidOperation(string msg) => new InvalidOperationException(msg);
    }
}
