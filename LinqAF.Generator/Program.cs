using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAF.Generator
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Expected [solution path] [template project name] [output project name] as arguments");
                return -1;
            }

            var slnPath = args[0];
            var templateName = args[1];
            var outputName = args[2];

            if (!File.Exists(slnPath))
            {
                Console.WriteLine($"Template solution not found at path {slnPath}");
                return -2;
            }
            
            if (Path.GetExtension(slnPath) != ".sln")
            {
                Console.WriteLine($"Template solution is not an .sln");
                return -3;
            }

            Action<string> log = txt => Console.Write(txt);

            string outProjectPath;

            var overall = new Stopwatch();
            overall.Start();

            var sw = new Stopwatch();
            sw.Start();
            log("Loading Projects... ");
            using (var overarching = Projects.Load(slnPath, templateName, outputName))
            {
                sw.Stop();

                log("Done ");
                log("(Took: " + sw.ElapsedMilliseconds + "ms)");
                log(Environment.NewLine);

                outProjectPath = overarching.Output.FilePath;

                Writer.Rewrite(overarching, log);

                log("Saving... ");

                sw.Restart();
                overarching.Save();
                sw.Stop();

                log("Done ");
                log("(Took: " + sw.ElapsedMilliseconds + "ms)");
                
                log(Environment.NewLine);
            }

            log("Nesting files... ");
            sw.Restart();
            FileNester.Nest(outProjectPath);
            sw.Stop();
            log("Done ");
            log("(Took: " + sw.ElapsedMilliseconds + "ms)");
            log(Environment.NewLine);

            overall.Stop();

            log("Total time: " + overall.ElapsedMilliseconds + "ms");
            log(Environment.NewLine);
            
            return 0;
        }
    }
}
