using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace LinqAF
{
    
    /// <summary>
    /// A drop in replacement for static calls to System.String
    /// 
    /// Methods that take IEnumerable now also take LinqAF enumerables.
    /// </summary>
    public static partial class LinqAFString // ugh, I hate this name
    {
        static dynamic PassThrough() { throw new NotImplementedException(); }

        // based on: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/String.Manipulation.cs
        // MIT Licensed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(Object arg0) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(Object arg0, Object arg1) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(Object arg0, Object arg1, Object arg2) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(Object arg0, Object arg1, Object arg2, Object arg3) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat(params object[] args) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat<T>(IEnumerable<T> values) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Concat(IEnumerable<string> values) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(String str0, String str1) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(String str0, String str1, String str2) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(String str0, String str1, String str2, String str3) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Concat(params String[] values) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(String format, Object arg0) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(String format, Object arg0, Object arg1) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(String format, Object arg0, Object arg1, Object arg2) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(String format, params Object[] args) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(IFormatProvider provider, String format, Object arg0) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(IFormatProvider provider, String format, Object arg0, Object arg1) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(IFormatProvider provider, String format, Object arg0, Object arg1, Object arg2) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Format(IFormatProvider provider, String format, params Object[] args) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string separator, params string[] value) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string separator, params object[] values)
        {
#if NET35
            if (values == null) Impl.CommonImplementation.ArgumentNull(nameof(values));
            return Join(separator, values.Select(s => s.ToString()));
#else
            return string.Join(separator, values);
#endif
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<T>(string separator, IEnumerable<T> values)
        {
#if NET35
            if (values == null) Impl.CommonImplementation.ArgumentNull(nameof(values));
            return Join(separator, values.Select(s => s.ToString()));
#else
            return string.Join(separator, values);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string separator, IEnumerable<string> values)
        {
#if NET35
            return Join(separator, Impl.CommonImplementation.Bridge(values, nameof(values)));
#else
            return string.Join(separator, values);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string separator, string[] value, int startIndex, int count) => PassThrough();

        // based on: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/String.cs
        // MIT Licensed

        public static readonly String Empty = string.Empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(String value) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhiteSpace(String value) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Copy(String str) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Intern(String str) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String IsInterned(String str) => PassThrough();

        // based on: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/String.Comparison.cs
        // MIT Licensed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, String strB) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, String strB, bool ignoreCase) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, String strB, StringComparison comparisonType) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, String strB, CultureInfo culture, CompareOptions options) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, String strB, bool ignoreCase, CultureInfo culture) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, int indexA, String strB, int indexB, int length) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, int indexA, String strB, int indexB, int length, bool ignoreCase) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, int indexA, String strB, int indexB, int length, bool ignoreCase, CultureInfo culture) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, int indexA, String strB, int indexB, int length, CultureInfo culture, CompareOptions options) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(String strA, int indexA, String strB, int indexB, int length, StringComparison comparisonType) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CompareOrdinal(String strA, String strB) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CompareOrdinal(String strA, int indexA, String strB, int indexB, int length) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(String a, String b) => PassThrough();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(String a, String b, StringComparison comparisonType) => PassThrough();
    }
}
