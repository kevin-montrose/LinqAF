using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace LinqAF
{
    public static partial class LinqAFTask
    {
        static dynamic PassThrough()
        {
            throw new NotImplementedException();
        }

        static dynamic PassThrough<TResult>()
        {
            throw new NotImplementedException();
        }

        // based on: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Tasks/Task.cs
        // MIT Licensed

        public static int? CurrentId => Task.CurrentId;

        public static TaskFactory Factory => Task.Factory;

        public static Task CompletedTask => Task.CompletedTask;

        public static YieldAwaitable Yield() => PassThrough();

        public static void WaitAll(params Task[] tasks) => PassThrough();

        public static bool WaitAll(Task[] tasks, TimeSpan timeout) => PassThrough();
        
        public static bool WaitAll(Task[] tasks, int millisecondsTimeout) => PassThrough();
        
        public static void WaitAll(Task[] tasks, CancellationToken cancellationToken) => PassThrough();

        public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken) => PassThrough();

        public static int WaitAny(params Task[] tasks) => PassThrough();
        
        public static int WaitAny(Task[] tasks, TimeSpan timeout) => PassThrough();
        
        public static int WaitAny(Task[] tasks, CancellationToken cancellationToken) => PassThrough();
       
        public static int WaitAny(Task[] tasks, int millisecondsTimeout) => PassThrough();
        
        public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken) => PassThrough();
        
        public static Task<TResult> FromResult<TResult>(TResult result) => PassThrough();
        
        public static Task FromException(Exception exception) => PassThrough();
        
        public static Task<TResult> FromException<TResult>(Exception exception) => PassThrough<TResult>();
        
        public static Task FromCanceled(CancellationToken cancellationToken) => PassThrough();
        
        public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken) => PassThrough<TResult>();
        
        public static Task Run(Action action) => PassThrough();
        
        public static Task Run(Action action, CancellationToken cancellationToken) => PassThrough();
        
        public static Task<TResult> Run<TResult>(Func<TResult> function) => PassThrough();
        
        public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken) => PassThrough();
        
        public static Task Run(Func<Task> function) => PassThrough();
        
        public static Task Run(Func<Task> function, CancellationToken cancellationToken) => PassThrough();
        
        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function) => PassThrough();
        
        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken) => PassThrough();
        
        public static Task Delay(TimeSpan delay) => PassThrough();
        
        public static Task Delay(TimeSpan delay, CancellationToken cancellationToken) => PassThrough();
        
        public static Task Delay(int millisecondsDelay) => PassThrough();
        
        public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken) => PassThrough();
        
        public static Task WhenAll(IEnumerable<Task> tasks) => PassThrough();
        
        public static Task WhenAll(params Task[] tasks) => PassThrough();

        public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks) => PassThrough();
        
        public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks) => PassThrough();
        
        public static Task<Task> WhenAny(params Task[] tasks) => PassThrough();
        
        public static Task<Task> WhenAny(IEnumerable<Task> tasks) => PassThrough();
        
        public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks) => PassThrough();
        
        public static Task<Task<TResult>> WhenAny<TResult>(IEnumerable<Task<TResult>> tasks) => PassThrough();
    }
}
