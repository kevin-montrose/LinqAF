using BenchmarkDotNet.Attributes;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LinqAF.Benchmark.Benchmarks
{
    public class Jil
    {
        public class IsAnonymouseClass
        {
            static readonly Type[] Types = MakeTypes();

            [Benchmark]
            public void LinqAF()
            {
                var e =
                    Types.Where(
                        type =>
                        {
                            // https://github.com/kevin-montrose/Jil/blob/master/Jil/Common/ExtensionMethods.cs#L384
                            if (IsValueType(type)) return false;
                            if (BaseType(type) != typeof(object)) return false;

                            var compilerGenerated = type.GetTypeInfo().CustomAttributes.Any(a => a.AttributeType == typeof(CompilerGeneratedAttribute));
                            if (!compilerGenerated) return false;

                            var allCons = type.GetConstructors();
                            if (allCons.Length != 1) return false;

                            var cons = allCons[0];
                            if (!cons.IsPublic) return false;

                            var props = type.GetProperties();
                            if (props.Any(p => p.CanWrite)) return false;

                            var propTypes = props.Select(t => t.PropertyType).ToList();

                            foreach (var param in cons.GetParameters())
                            {
                                // this Contains isn't a Linq call
                                if (!propTypes.Contains(param.ParameterType)) return false;

                                propTypes.Remove(param.ParameterType);
                            }

                            if (propTypes.Count != 0) return false;

                            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                            if (fields.Any(f => !f.IsPrivate)) return false;

                            propTypes = props.Select(t => t.PropertyType).ToList();
                            foreach (var field in fields)
                            {
                                // this Contains isn't a Linq call
                                if (!propTypes.Contains(field.FieldType)) return false;

                                propTypes.Remove(field.FieldType);
                            }

                            if (propTypes.Count != 0) return false;

                            var equals = type.GetMethod("Equals", new Type[] { typeof(object) });
                            var hashCode = type.GetMethod("GetHashCode", new Type[0]);

                            if (!IsOverride(equals) || !IsOverride(hashCode)) return false;

                            return true;
                        }
                    );
                foreach(var i in e)
                {
                    GC.KeepAlive(i);
                }
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var e =
                    System.Linq.Enumerable.Where(
                        Types,
                        type =>
                        {
                            // https://github.com/kevin-montrose/Jil/blob/master/Jil/Common/ExtensionMethods.cs#L384
                            if (IsValueType(type)) return false;
                            if (BaseType(type) != typeof(object)) return false;

                            var compilerGenerated = System.Linq.Enumerable.Any(type.GetTypeInfo().CustomAttributes, a => a.AttributeType == typeof(CompilerGeneratedAttribute));
                            if (!compilerGenerated) return false;

                            var allCons = type.GetConstructors();
                            if (allCons.Length != 1) return false;

                            var cons = allCons[0];
                            if (!cons.IsPublic) return false;

                            var props = type.GetProperties();
                            if (System.Linq.Enumerable.Any(props, p => p.CanWrite)) return false;

                            var propTypes =
                                System.Linq.Enumerable.ToList(
                                    System.Linq.Enumerable.Select(
                                        props,
                                        t => t.PropertyType
                                    )
                                );

                            foreach (var param in cons.GetParameters())
                            {
                                // this Contains isn't a Linq call
                                if (!propTypes.Contains(param.ParameterType)) return false;

                                propTypes.Remove(param.ParameterType);
                            }

                            if (propTypes.Count != 0) return false;

                            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                            if (System.Linq.Enumerable.Any(fields, f => !f.IsPrivate)) return false;

                            propTypes =
                                System.Linq.Enumerable.ToList(
                                    System.Linq.Enumerable.Select(
                                        props,
                                        t => t.PropertyType
                                    )
                                );
                            foreach (var field in fields)
                            {
                                // this Contains isn't a Linq call
                                if (!propTypes.Contains(field.FieldType)) return false;

                                propTypes.Remove(field.FieldType);
                            }

                            if (propTypes.Count != 0) return false;

                            var equals = type.GetMethod("Equals", new Type[] { typeof(object) });
                            var hashCode = type.GetMethod("GetHashCode", new Type[0]);

                            if (!IsOverride(equals) || !IsOverride(hashCode)) return false;

                            return true;
                        }
                    );
                foreach (var i in e)
                {
                    GC.KeepAlive(i);
                }
            }

            static Type[] MakeTypes()
            {
                return
                    new[]
                    {
                        typeof(string),
                        typeof(object),
                        new { Foo = "string" }.GetType(),
                        new { Int = 1, Double = 0.0 }.GetType(),
                        typeof(int),
                        typeof(BenchmarkAttribute),
                        typeof(System.Collections.Generic.Dictionary<string, string>)
                    };
            }

            static bool IsValueType(Type type)
            {
                var info = type.GetTypeInfo();

                return info.IsValueType;
            }

            static Type BaseType(Type type)
            {
                var info = type.GetTypeInfo();

                return info.BaseType;
            }

            static bool IsOverride(MethodInfo method)
            {
                return method.GetBaseDefinition() != method;
            }
        }

    }
}
