using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace LinqAF.Generator
{
    static class Copier
    {
        public static void Copy(Document doc, string folder, Projects into)
        {
            var name = doc.Name;
            var text = doc.GetTextAsync().Result;

            into.ModifyOutput(p => p.AddDocument(name, text, folder == null ? null : new[] { folder }).Project);
        }
    }
}
