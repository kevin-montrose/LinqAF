using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LinqAF.Tests
{
    [TestClass]
    public class QueryableTests
    {
        [TestMethod]
        public void Compatible()
        {
            var l2oInterface = typeof(System.Linq.Queryable);
            var lafInterface = typeof(LinqAF.Queryable);

            var l2oPubMtds = l2oInterface.GetMethods(BindingFlags.Public | BindingFlags.Static);
            var lafPubMtds = lafInterface.GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(l2oPubMtds.Count(), lafPubMtds.Count());

            var distinctMatches = new HashSet<MethodInfo>();

            foreach(var l2oMtd in l2oPubMtds)
            {
                var l2oRet = l2oMtd.ReturnType;
                var l2oParams = l2oMtd.GetParameters();

                var candidateLafMtds = lafPubMtds.Where(p => p.Name == l2oMtd.Name && p.GetParameters().Length == l2oParams.Length);

                MethodInfo matching = null;

                foreach(var candidateLafMtd in candidateLafMtds)
                {
                    var lafRet = candidateLafMtd.ReturnType;

                    if (!Equivalent(l2oRet, lafRet)) continue;
   
                    var lafParams = candidateLafMtd.GetParameters();

                    var matched = true;

                    for(var i =0; i < l2oParams.Length; i++)
                    {
                        if(!Equivalent(l2oParams[i].ParameterType, lafParams[i].ParameterType))
                        {
                            matched = false;
                            break;
                        }
                    }

                    if (!matched) continue;

                    matching = candidateLafMtd;
                    break;
                }

                if (matching == null) throw new Exception("No equivalent found for " + l2oMtd.Name);
                if (!distinctMatches.Add(matching)) throw new Exception("Same method matched multiple times!");
                if (matching.GetCustomAttribute<ExtensionAttribute>() == null) throw new Exception("Matching method isn't an extension method");
            }
        }

        static bool Equivalent(Type a, Type b)
        {
            if (a == b) return true;
            if (a.IsEquivalentTo(b) && b.IsEquivalentTo(a)) return true;
            if (a.IsAssignableFrom(b) && b.IsAssignableFrom(a)) return true;

            // eh, not strictly true 
            if (a.IsGenericParameter && b.IsGenericParameter) return true;

            if (!a.IsGenericType && !b.IsGenericType) return false;
            if (a.IsGenericType && !b.IsGenericType) return false;
            if (!a.IsGenericType && b.IsGenericType) return false;
            
            var aParams = a.GetGenericArguments();
            var bParams = b.GetGenericArguments();

            if (aParams?.Length != bParams?.Length) return false;
            
            for(var i = 0; i < aParams.Length; i++)
            {
                var aP = aParams[i];
                var bP = bParams[i];

                if (!Equivalent(aP, bP)) return false;
            }

            return true;
        }
    }
}
