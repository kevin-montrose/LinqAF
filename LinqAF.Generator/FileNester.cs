using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LinqAF.Generator
{
    static class FileNester
    {
        public static void Nest(string csProjPath)
        {
            var doc = new XmlDocument();
            doc.Load(csProjPath);

            var compile = LoadCompileElements(doc);
            var groups = GroupFileNodes(compile);

            AddDependencies(doc, groups);

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            using (var xml = XmlWriter.Create(sb, settings))
            {
                doc.WriteTo(xml);
            }

            var updatedCsproj = sb.ToString();

            File.WriteAllText(csProjPath, updatedCsproj);
        }

        static List<XmlNode> LoadCompileElements(XmlDocument doc)
        {
            var compile = new List<XmlNode>();

            var pending = new Stack<XmlNode>();
            pending.Push(doc);

            while (pending.Count > 0)
            {
                var next = pending.Pop();

                if (next.HasChildNodes)
                {
                    foreach (XmlNode p in next.ChildNodes)
                    {
                        pending.Push(p);
                    }
                }

                if (next.NodeType != XmlNodeType.Element) continue;

                if (next.Name == "Compile")
                {
                    compile.Add(next);
                }
            }

            return compile;
        }

        static Dictionary<string, List<XmlNode>> GroupFileNodes(List<XmlNode> compile)
        {
            var groups = new Dictionary<string, List<XmlNode>>();

            var canGroup = compile.Where(g => g.Attributes["Include"].Value.Count(x => x == '.') > 1);
            foreach (var subFile in canGroup)
            {
                var include = subFile.Attributes["Include"];
                if (include == null || string.IsNullOrEmpty(include.Value)) continue;

                var name = subFile.Attributes["Include"].Value;
                var start = name.IndexOf('\\') + 1;              // either 0, or the char after the \
                var end = name.IndexOf('.');

                var groupUnder = name.Substring(start, end - start) + ".cs";
                List<XmlNode> dependents;
                if (!groups.TryGetValue(groupUnder, out dependents))
                {
                    groups[groupUnder] = dependents = new List<XmlNode>();
                }

                dependents.Add(subFile);
            }

            return groups;
        }

        static void AddDependencies(XmlDocument doc, Dictionary<string, List<XmlNode>> groups)
        {
            foreach (var group in groups)
            {
                var makeDependentUpon = group.Key;

                var nodes = group.Value;

                foreach (var node in nodes)
                {
                    var depedentNode = doc.CreateElement("DependentUpon", doc.DocumentElement.NamespaceURI);
                    depedentNode.InnerText = makeDependentUpon;

                    node.AppendChild(depedentNode);
                }
            }
        }
    }
}