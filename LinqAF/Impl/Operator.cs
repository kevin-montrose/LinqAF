// From: MiscUtil http://jonskeet.uk/csharp/miscutil/
// By: Jon Skeet & Marc Gravell
// License: Apache

using MiscUtil.Linq;
using System;
using System.Linq.Expressions;

namespace MiscUtil
{
    /// <summary>
    /// The Operator class provides easy access to the standard operators
    /// (addition, etc) for generic types, using type inference to simplify
    /// usage.
    /// </summary>
    static class Operator
    {
        /// <summary>
        /// Performs a conversion between the given types; this will throw
        /// an InvalidOperationException if the type T does not provide a suitable cast, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this cast.
        /// </summary>
        public static TTo Convert<TFrom, TTo>(TFrom value)
        {
            return Operator<TFrom, TTo>.Convert(value);
        }
        /// <summary>
        /// Evaluates binary addition (+) for the given type; this will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>        
        public static T Add<T>(T value1, T value2)
        {
            return Operator<T>.Add(value1, value2);
        }
        /// <summary>
        /// Evaluates binary subtraction (-) for the given type; this will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static T Subtract<T>(T value1, T value2)
        {
            return Operator<T>.Subtract(value1, value2);
        }
        /// <summary>
        /// Evaluates binary equality (==) for the given type; this will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static bool Equal<T>(T value1, T value2)
        {
            return Operator<T>.Equal(value1, value2);
        }
        /// <summary>
        /// Evaluates binary less-than (&lt;) for the given type; this will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static bool LessThan<T>(T value1, T value2)
        {
            return Operator<T>.LessThan(value1, value2);
        }
        /// <summary>
        /// Evaluates binary greater-than-on-eqauls (&gt;=) for the given type; this will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static bool GreaterThanOrEqual<T>(T value1, T value2)
        {
            return Operator<T>.GreaterThanOrEqual(value1, value2);
        }
    }
    /// <summary>
    /// Provides standard operators (such as addition) that operate over operands of
    /// different types. For operators, the return type is assumed to match the first
    /// operand.
    /// </summary>
    /// <seealso cref="Operator&lt;T&gt;"/>
    /// <seealso cref="Operator"/>
    public static class Operator<TValue, TResult>
    {
        private static readonly Func<TValue, TResult> convert;
        /// <summary>
        /// Returns a delegate to convert a value between two types; this delegate will throw
        /// an InvalidOperationException if the type T does not provide a suitable cast, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this cast.
        /// </summary>
        public static Func<TValue, TResult> Convert { get { return convert; } }
        static Operator()
        {
            convert = ExpressionUtil.CreateExpression<TValue, TResult>(body => Expression.Convert(body, typeof(TResult)));
        }
    }

    /// <summary>
    /// Provides standard operators (such as addition) over a single type
    /// </summary>
    /// <seealso cref="Operator"/>
    /// <seealso cref="Operator&lt;TValue,TResult&gt;"/>
    public static class Operator<T>
    {
        static readonly T zero;
        /// <summary>
        /// Returns the zero value for value-types (even full Nullable&lt;TInner&gt;) - or null for reference types
        /// </summary>
        public static T Zero { get { return zero;} }

        static readonly Func<T, T, T> add, subtract;
        /// <summary>
        /// Returns a delegate to evaluate binary addition (+) for the given type; this delegate will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static Func<T, T, T> Add { get { return add; } }
        /// <summary>
        /// Returns a delegate to evaluate binary subtraction (-) for the given type; this delegate will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static Func<T, T, T> Subtract { get { return subtract; } }


        static readonly Func<T, T, bool> equal, lessThan, greaterThanOrEqual;
        /// <summary>
        /// Returns a delegate to evaluate binary equality (==) for the given type; this delegate will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static Func<T, T, bool> Equal { get { return equal; } }
        /// <summary>
        /// Returns a delegate to evaluate binary less-than (&lt;) for the given type; this delegate will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static Func<T, T, bool> LessThan { get { return lessThan; } }
        /// <summary>
        /// Returns a delegate to evaluate binary greater-than-or-equal (&gt;=) for the given type; this delegate will throw
        /// an InvalidOperationException if the type T does not provide this operator, or for
        /// Nullable&lt;TInner&gt; if TInner does not provide this operator.
        /// </summary>
        public static Func<T, T, bool> GreaterThanOrEqual { get { return greaterThanOrEqual; } }

        static Operator()
        {
            add = ExpressionUtil.CreateExpression<T, T, T>(Expression.Add);
            subtract = ExpressionUtil.CreateExpression<T, T, T>(Expression.Subtract);
            
            greaterThanOrEqual = ExpressionUtil.CreateExpression<T, T, bool>(Expression.GreaterThanOrEqual);
            lessThan = ExpressionUtil.CreateExpression<T, T, bool>(Expression.LessThan);
            equal = ExpressionUtil.CreateExpression<T, T, bool>(Expression.Equal);
            
            Type typeT = typeof(T);
            if(typeT.IsValueType && typeT.IsGenericType && (typeT.GetGenericTypeDefinition() == typeof(Nullable<>))) {
                // get the *inner* zero (not a null Nullable<TValue>, but default(TValue))
                Type nullType = typeT.GetGenericArguments()[0];
                zero = (T)Activator.CreateInstance(nullType);
            } else {
                zero = default(T);
            }
        }
    }
}
