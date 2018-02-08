using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Jil;
using System.Threading;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using System.Threading.Tasks;

namespace LinqAF.TestRunner
{
    class Program
    {
        static readonly object[] Empty = new object[0];

        delegate bool RunTestDelegate(out List<Covered> coverage, out string errorMessage);
        struct Test
        {
            public string Name { get; set; }
            public RunTestDelegate Run { get; set; }
        }

        class TestResult
        {
            public string ErrorMessage { get; set; }
            public List<Covered> Coverage { get; set; }
        }

        static void Main(string[] args)
        {
            const bool defaultSuppressCoverage = true;  // set false to collect coverage, true to not

            var run = new List<Test>();
            var asm = Assembly.Load("LinqAF.Tests");

            List<Covered> covered;
            Dictionary<string, TestResult> results;

            var masterProcess = false;

            IDisposable redirecting;

            if (args.Length == 0)
            {
                redirecting = RedirectOut();

                var tests = new List<string>();

                foreach (var @class in asm.GetTypes())
                {
                    if (@class.GetCustomAttribute<TestClassAttribute>() != null)
                    {
                        tests.AddRange(QueueTests(@class, suppressCoverage: defaultSuppressCoverage).Select(t => t.Name).AsEnumerable());
                    }
                }

                tests = tests.OrderBy(x => x).ToList();
                
                Console.WriteLine($"Queuing {tests.Count} tests on {UseProceses()} different processes");
                
                results = ShardAndCollect(tests, out covered);
                masterProcess = true;
            }
            else
            {
                redirecting = null;

                covered = null;
                var suppressCoverage = args.Length > 1 ? args[1] == "no-coverage" : defaultSuppressCoverage;

                var testNames = new HashSet<string>(args[0].Split(';'));

                var classes = testNames.Select(t => t.Substring(0, t.IndexOf('.'))).ToList();

                foreach (var @class in asm.GetTypes())
                {
                    if (@class.GetCustomAttribute<TestClassAttribute>() != null)
                    {
                        if (classes.Contains(@class.Name))
                        {
                            var testsOnClass = QueueTests(@class, suppressCoverage);
                            var toRun = testsOnClass.Where(c => testNames.Contains(c.Name)).AsEnumerable();

                            run.AddRange(toRun);
                        }
                    }
                }

                results = new Dictionary<string, TestResult>();

                foreach (var test in run.OrderBy(r => r.Name))
                {
                    List<Covered> coverage;
                    string errorMessage;
                    if (!test.Run(out coverage, out errorMessage))
                    {
                        results[test.Name] =
                            new TestResult
                            {
                                ErrorMessage = errorMessage,
                                Coverage = null
                            };
                    }
                    else
                    {
                        results[test.Name] =
                            new TestResult
                            {
                                ErrorMessage = null,
                                Coverage = coverage
                            };
                    }
                }

                var data = results.Select(kv => new { TestName = kv.Key, ErrorMessage = kv.Value.ErrorMessage, TestCoverageEncoded = Covered.EncodeList(kv.Value.Coverage) }).ToList();
                var json = JSON.Serialize(data, Options.PrettyPrint);
                Console.Error.Write(json);
            }

            var passedCount = results.Count(kv => kv.Value.ErrorMessage == null);
            var runCount = results.Count;

            Console.WriteLine($"{passedCount}/{runCount} tests passed");
            foreach (var kv in results)
            {
                if (kv.Value.ErrorMessage != null)
                {
                    Console.WriteLine($"Failed: {kv.Key}");
                    Console.WriteLine(kv.Value.ErrorMessage);
                }
            }

            if (masterProcess)
            {
                Console.WriteLine();

                if (covered != null)
                {
                    covered.Sort((x, y) => -(x.CoveragePercent.CompareTo(y.CoveragePercent)));

                    var totalMethods = covered.Count;
                    var at100 = covered.Count(c => c.CoveragePercent >= 100);
                    var covPer = ((double)at100) / (double)totalMethods * 100.0;
                    Console.WriteLine($"{at100}/{totalMethods} methods ({Math.Round(covPer, 1)}%) at 100% code coverage");

                    Console.WriteLine("Press any key to dump 0% coverage methods");
                    Console.ReadKey();

                    foreach (var method in covered.Where(p => p.CoveragePercent <= 0))
                    {
                        Console.WriteLine($"\t{method.NiceMethodName} at {Math.Round(method.CoveragePercent, 1)}% coverage");
                    }
                }

#if DEBUG
                Console.WriteLine();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
#endif
            }

            redirecting?.Dispose();
        }

        class EchoStream: TextWriter
        {
            TextWriter Left;
            TextWriter Right;

            public EchoStream(TextWriter left, TextWriter right)
            {
                Left = left;
                Right = right;
            }

            public override Encoding Encoding => Left.Encoding;

            public override void Write(char value)
            {
                Left.Write(value);
                Right.Write(value);

                Left.Flush();
                Right.Flush();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    Left.Flush();
                    Right.Flush();

                    Left.Dispose();
                    Right.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        static IDisposable RedirectOut()
        {
            var oldOut = Console.Out;

            var newOutFs = File.Open(@".\log.txt", FileMode.Create, FileAccess.Write, FileShare.Read);
            var newOut = new StreamWriter(newOutFs);

            var echo = new EchoStream(oldOut, newOut);
            Console.SetOut(echo);

            return echo;
        }

        static int UseProceses()
        {
            var ret = Environment.ProcessorCount;

            // FULL BLAST
            return ret;

            // PARTIAL BLAST
            //ret /= 2;
            //ret--;
            //if (ret < 0) ret = 1;

            //return ret;
        }

        static async Task<List<Covered>> CollectCoverageAsync(string test)
        {
            const string PATH_TO_OPEN_COVER = @"..\..\..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe";

            string currentProcFile;
            using (var proc = Process.GetCurrentProcess())
            {
                currentProcFile = proc.MainModule.FileName;
            }

            var outputFile = Path.GetTempFileName();

            try
            {
                var procStart = new ProcessStartInfo();
                procStart.CreateNoWindow = true;
                procStart.FileName = PATH_TO_OPEN_COVER;
                procStart.Arguments =
                    string.Format(
                        "-target:\"{0}\" -targetargs:\"{1} no-coverage\" -output:\"{2}\" -skipautoprops -register:user -filter:\"+[LinqAF.*]* -[LinqAF.Tests]* -[LinqAF.TestRunner]* -[TestHelpers]* -[MiscUtil]*\"",
                        currentProcFile,
                        test,
                        outputFile
                    );
                procStart.WorkingDirectory = Path.GetDirectoryName(outputFile);
                procStart.UseShellExecute = false;
                
                using (var coverageProc = Process.Start(procStart))
                {
                    while (!coverageProc.HasExited)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                }

                var xml = new XmlDocument();
                xml.Load(outputFile);

                var parsedCoverage = new List<Covered>();

                var modules = xml.DocumentElement.GetElementsByTagName("Module").Cast<XmlElement>();
                foreach (var module in modules)
                {
                    var covered = module.GetElementsByTagName("Method").Cast<XmlElement>();

                    foreach (var elem in covered)
                    {
                        var nameElem = elem.GetElementsByTagName("Name").Cast<XmlElement>().First();
                        var name = nameElem.InnerText;

                        var seqPointElems = elem.GetElementsByTagName("SequencePoint").Cast<XmlElement>();
                        var seqPoints = new List<SequencePoint>();
                        foreach (var selem in seqPointElems)
                        {
                            var visited = int.Parse(selem.Attributes["vc"].InnerText);
                            var ordinal = int.Parse(selem.Attributes["ordinal"].InnerText);
                            seqPoints.Add(new SequencePoint { Visited = visited > 0, Ordinal = ordinal });
                        }

                        parsedCoverage.Add(new Covered(name, seqPoints.ToArray()));
                    }
                }

                var keepCoverage = parsedCoverage.Where(p => p.SequencePoints.Any()).ToList();
                
                return keepCoverage;
            }
            finally
            {
                File.Delete(outputFile);
            }
        }

        class SequencePoint
        {
            public bool Visited { get; set; }
            public int Ordinal { get; set; }

            public override string ToString() => $"@{Ordinal}: {Visited}";
        }

        class Covered
        {
            public string NiceMethodName
            {
                get
                {
                    var firstSpace = MethodName.IndexOf(' ');
                    var proper = MethodName.Substring(firstSpace + 1);

                    return proper;
                }
            }

            public string MethodName { get; private set; }
            public SequencePoint[] SequencePoints { get; private set; }

            public double CoveragePercent { get; private set; }

            public Covered(string name, SequencePoint[] points)
            {
                MethodName = name;
                SequencePoints = points;
                CoveragePercent = ((double)SequencePoints.Count(s => s.Visited)) / (double)SequencePoints.Length * 100.0;
            }
            
            public Covered Merge(Covered other)
            {
                if (other.MethodName != MethodName) throw new Exception("Can't merge with different names");
                if (other.SequencePoints.Length != SequencePoints.Length) throw new Exception("uhhhh");

                var inner = new SequencePoint[SequencePoints.Length];
                for(var i =0; i < SequencePoints.Length; i++)
                {
                    var selfPoint= SequencePoints[i];
                    var otherPoint = other.SequencePoints[i];
                    if (selfPoint.Visited)
                    {
                        inner[i] = selfPoint;
                    }
                    else
                    {
                        inner[i] = otherPoint;
                    }
                }

                return new Covered(MethodName, inner);
            }

            public static IEnumerable<Covered> Merge(IEnumerable<Covered> all)
            {
                var ret = new Dictionary<string, Covered>();
                foreach(var i in all)
                {
                    if (i == null) continue;

                    Covered previous;
                    if(!ret.TryGetValue(i.MethodName, out previous))
                    {
                        ret[i.MethodName] = i;
                        continue;
                    }

                    var merged = previous.Merge(i);
                    ret[i.MethodName] = merged;
                }

                return ret.Values;
            }

            public static Covered Parse(byte[] bytes)
            {
                if (bytes == null) return null;

                var methodNameLen = (bytes[0] << 8) | (bytes[1]);
                var name = Encoding.UTF8.GetString(bytes, 2, methodNameLen);

                var maxOrdinal = (bytes[2 + methodNameLen] << 8) | bytes[2 + methodNameLen + 1];

                System.Collections.BitArray bitMask;
                if (bytes.Length > 2 + methodNameLen + 2)
                {
                    var byteCount = ((maxOrdinal + 1) / 8) + ((maxOrdinal + 1) % 8);
                    var bitMaskBytes = new byte[byteCount];
                    Array.Copy(bytes, 2 + methodNameLen + 2, bitMaskBytes, 0, byteCount);

                    bitMask = new System.Collections.BitArray(bitMaskBytes);
                }
                else
                {
                    bitMask = null;
                }

                var sequencePoints = new SequencePoint[maxOrdinal + 1];
                for (var i = 0; i <= maxOrdinal; i++)
                {
                    sequencePoints[i] = new SequencePoint { Ordinal = i, Visited = bitMask?.Get(i) ?? false };
                }

                return new Covered(name, sequencePoints);
            }

            public static IEnumerable<Covered> DecodeList(string encoded)
            {
                if (encoded == null) return null;

                var compressedBytes = Convert.FromBase64String(encoded);

                byte[] bytesArr;
                using (var outMem = new MemoryStream())
                {
                    using (var inMem = new MemoryStream(compressedBytes))
                    using (var gzip = new System.IO.Compression.GZipStream(inMem, System.IO.Compression.CompressionMode.Decompress))
                    {
                        gzip.CopyTo(outMem);
                    }
                    bytesArr = outMem.ToArray();
                }
                
                var ret = new LinkedList<Covered>();
                var ix = 0;
                while(ix < bytesArr.Length)
                {
                    var len = (bytesArr[ix] << 8) | bytesArr[ix + 1];

                    var arr = new byte[len];
                    Array.Copy(bytesArr, ix + 2, arr, 0, len);

                    ret.AddLast(Parse(arr));

                    ix += 2 + len;
                }

                return ret;
            }

            public static string EncodeList(IEnumerable<Covered> data)
            {
                if (data == null) return null;

                var ret = new LinkedList<IEnumerable<byte>>();
                foreach (var c in data)
                {
                    var asBytes = c.Encode();
                    var length = System.Linq.Enumerable.Count(asBytes);
                    if (length > ushort.MaxValue) throw new Exception("Giant coverage array, what");

                    var len1 = (byte)((length >> 8) & 0xFF);
                    var len2 = (byte)((length >> 0) & 0xFF);

                    ret.AddLast(new[] { len1, len2 });
                    ret.AddLast(asBytes);
                }

                byte[] compressed;
                using (var mem = new MemoryStream())
                {
                    using (var gzip = new System.IO.Compression.GZipStream(mem, System.IO.Compression.CompressionLevel.Optimal))
                    {
                        foreach (var bytes in ret)
                        {
                            foreach(var @byte in bytes)
                            {
                                gzip.WriteByte(@byte);
                            }
                        }
                    }

                    compressed = mem.ToArray();
                }

                return Convert.ToBase64String(compressed);
            }

            public IEnumerable<byte> Encode()
            {
                var allSbs = new List<List<byte>>();
                var curSb = new List<byte>();
                allSbs.Add(curSb);
                Action<byte> addByte =
                    b =>
                    {
                        curSb.Add(b);
                        if(curSb.Count >= int.MaxValue / 2)
                        {
                            var newSb = new List<byte>();
                            allSbs.Add(newSb);
                            curSb = newSb;
                        }
                    };
                Action<IEnumerable<byte>> addByteRange =
                    e =>
                    {
                        foreach (var b in e)
                        {
                            addByte(b);
                        }
                    };

                var methodNameBytes = Encoding.UTF8.GetBytes(MethodName);
                var len = methodNameBytes.Length;
                if (len > ushort.MaxValue) throw new Exception("Yeah, that shouldn't be possible");
                addByte((byte)((len >> 8) & 0xFF));
                addByte((byte)((len >> 0) & 0xFF));
                addByteRange(methodNameBytes);

                var maxOrdinal = SequencePoints.Max(s => s.Ordinal);
                if (maxOrdinal > ushort.MaxValue) throw new Exception("That's a giant method, wtf");
                addByte((byte)((maxOrdinal >> 8) & 0xFF));
                addByte((byte)((maxOrdinal >> 0) & 0xFF));

                if (SequencePoints.Any(s => s.Visited))
                {
                    var bitMask = new System.Collections.BitArray(maxOrdinal + 1);
                    foreach (var seq in SequencePoints)
                    {
                        if (seq.Visited)
                        {
                            bitMask.Set(seq.Ordinal, true);
                        }
                    }

                    var bitArrayByteCount = ((maxOrdinal + 1) / 8) + ((maxOrdinal + 1) % 8);
                    var bitArrayBytes = new byte[bitArrayByteCount];
                    bitMask.CopyTo(bitArrayBytes, 0);
                    addByteRange(bitArrayBytes);
                }

                foreach(var set in allSbs)
                {
                    foreach(var b in set)
                    {
                        yield return b;
                    }
                }
            }

            public override string ToString()
            {
                return $@"{CoveragePercent}%: {MethodName}
---
{string.Join("\r\n", SequencePoints.Select(s => s.ToString()).AsEnumerable())}";
            }
        }

        static Dictionary<string, TestResult> ShardAndCollect(List<string> testNames, out List<Covered> covered)
        {
            var finished = 0;

            // all this just so we can make sure the subprocesses die when their parent does
            var _ = new Extern.SECURITY_ATTRIBUTES { };
            var job = Extern.CreateJobObject(ref _, "LinqAF.TestRunner");
            var info = new Extern.JOBOBJECT_BASIC_LIMIT_INFORMATION();
            info.LimitFlags = 0x2000;   // JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE

            var extendedInfo = new Extern.JOBOBJECT_EXTENDED_LIMIT_INFORMATION();
            extendedInfo.BasicLimitInformation = info;

            int length = Marshal.SizeOf(typeof(Extern.JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            var extendedInfoPtr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);

            if (!Extern.SetInformationJobObject(job, Extern.JobObjectInfoType.ExtendedLimitInformation, extendedInfoPtr, (uint)length))
            {
                throw new Exception("Wat");
            }

            var results = new Dictionary<string, TestResult>();

            var runningProcesses = new List<Tuple<Process, Thread, Thread>>();
            var pendingProcessStarts = new Queue<ProcessStartInfo>();

            string currentProcFile;
            using (var proc = Process.GetCurrentProcess())
            {
                currentProcFile = proc.MainModule.FileName;
            }

            var coveredRefLock = new object();
            IEnumerable<Covered> coveredRef = null;

            foreach (var name in testNames)
            {
                var procStart =
                    new ProcessStartInfo
                    {
                        FileName = currentProcFile,
                        Arguments = name,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    };

                pendingProcessStarts.Enqueue(procStart);
            }

            Func<int> getPendingProcessStartsCount =
                () =>
                {
                    lock (pendingProcessStarts)
                    {
                        return pendingProcessStarts.Count;
                    }
                };

            while (getPendingProcessStartsCount() > 0 || runningProcesses.Count > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                for (var i = runningProcesses.Count - 1; i >= 0; i--)
                {
                    var t = runningProcesses[i];
                    var proc = t.Item1;
                    var outThread = t.Item2;
                    var errThread = t.Item3;

                    if (!outThread.IsAlive && !errThread.IsAlive && proc.HasExited)
                    {
                        runningProcesses.RemoveAt(i);
                        proc.Dispose();
                    }
                }

                if (runningProcesses.Count >= UseProceses())
                {
                    continue;
                }

                if (getPendingProcessStartsCount() > 0)
                {
                    ProcessStartInfo next;

                    lock (pendingProcessStarts)
                    {
                        next = pendingProcessStarts.Dequeue();
                    }
                    var newProc = Process.Start(next);

                    Extern.AssignProcessToJobObject(job, newProc.Handle);

                    if (Debugger.IsAttached)
                    {
                        AttachDebugger(newProc);
                    }

                    var outThread =
                        new Thread(
                            () =>
                            {
                                while (!newProc.HasExited)
                                {
                                    // need to read it off, but needn't actually print it
                                    var toWrite = newProc.StandardOutput.ReadLine();
                                    //Console.WriteLine($"[Proc: {newProc.Id}]: {toWrite}");
                                }
                            }
                        );

                    var errThread =
                        new Thread(
                            () =>
                            {
                                var needsHandling = new StringBuilder();

                                while (!newProc.HasExited)
                                {
                                    int c;
                                    while ((c = newProc.StandardError.Read()) != -1)
                                    {
                                        needsHandling.Append((char)c);
                                    }
                                }

                                var json = needsHandling.ToString();
                                dynamic data;

                                try
                                {
                                    data = JSON.DeserializeDynamic(json);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"[Proc: {newProc.Id}] ({next.Arguments}) ~~CRASHED~~");
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine(e.StackTrace);

                                    Console.WriteLine("Rescheduling");
                                    lock (pendingProcessStarts)
                                    {
                                        pendingProcessStarts.Enqueue(next);
                                    }

                                    return;
                                }

                                foreach (var entry in data)
                                {
                                    string name = entry.TestName;
                                    string errorMessage = entry.ErrorMessage;
                                    string testCoverageEncoded = entry.TestCoverageEncoded;
                                    lock (results)
                                    {
                                        results[name] =
                                            new TestResult
                                            {
                                                ErrorMessage = errorMessage,
                                                Coverage = null
                                            };
                                    }

                                    double coverageRate;

                                    var parsedCoverage = Covered.DecodeList(testCoverageEncoded);
                                    if (parsedCoverage != null)
                                    {
                                        lock (coveredRefLock)
                                        {
                                            if (coveredRef == null)
                                            {
                                                coveredRef = parsedCoverage;
                                                coverageRate = double.NaN;
                                            }
                                            else
                                            {
                                                coveredRef = Covered.Merge(coveredRef.Concat(parsedCoverage).AsEnumerable());
                                            }

                                            var total100 = coveredRef.Count(c => c.CoveragePercent >= 100);
                                            var total = coveredRef.Count();

                                            coverageRate = Math.Round(((double)total100) / (double)total * 100.0, 1);
                                        }
                                    }
                                    else
                                    {
                                        coverageRate = double.NaN;
                                    }

                                    var num = Interlocked.Increment(ref finished);

                                    if (errorMessage == null)
                                    {
                                        if (!double.IsNaN(coverageRate))
                                        {
                                            Console.WriteLine($"[Proc: {newProc.Id}] ({num}/{testNames.Count}): {name} PASSED (covered methods: {coverageRate}%)");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"[Proc: {newProc.Id}] ({num}/{testNames.Count}): {name} PASSED");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"[Proc: {newProc.Id}] ({num}/{testNames.Count}): {name} !!FAILED!!");
                                    }
                                }
                            }
                        );

                    outThread.IsBackground = true;
                    errThread.IsBackground = true;

                    outThread.Start();
                    errThread.Start();

                    runningProcesses.Add(Tuple.Create(newProc, outThread, errThread));
                }
            }

            covered = coveredRef?.ToList();
            return results;
        }

        static void AttachDebugger(Process proc)
        {
            try
            {
                MessageFilter.Register();
                var dte = GetVisualStudioReference();

                // didn't get a visual studio reference
                if (dte == null) return;

                var vsProcs = RetryOnVisualStudioBusy(() => dte.Debugger.LocalProcesses);
                int total = RetryOnVisualStudioBusy(() => vsProcs.Count);

                dynamic target = null;
                for (var i = 0; i < total; i++)
                {
                    try
                    {
                        var vsProc = RetryOnVisualStudioBusy(() => vsProcs.Item(i));
                        int vsProcId = RetryOnVisualStudioBusy(() => vsProc.ProcessID);

                        if (vsProcId == proc.Id)
                        {
                            target = vsProc;
                            break;
                        }
                    }
                    catch
                    {
                        /* whelp */
                    }
                }

                if (target != null)
                {
                    RetryOnVisualStudioBusy(() => { target.Attach(); return null; });
                }

                try { MessageFilter.Revoke(); }
                catch
                {
                    /* don't care */
                }
            }
            catch
            {
                throw;
            }
        }

        static dynamic RetryOnVisualStudioBusy(Func<dynamic> act)
        {
            while (true)
            {
                try
                {
                    return act();
                }
                catch (COMException cex)
                {
                    // if VS is just "busy" give it another go
                    if (((uint)cex.ErrorCode) == 0x8001010A)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    // otherwise, throw that shit
                    throw;
                }
            }
        }

        static dynamic GetVisualStudioReference()
        {
            var vses = GetVisualStudioInstances();

            foreach (var vs in vses)
            {
                try
                {
                    var solution = vs.Solution;
                    string name = solution.FullName;

                    var sln = Path.GetFileNameWithoutExtension(name);
                    if (sln == "LinqAF")
                    {
                        return vs;
                    }
                }
                catch { }
            }

            return null;
        }

        static IEnumerable<dynamic> GetVisualStudioInstances()
        {
            IRunningObjectTable rot;
            IEnumMoniker enumMoniker;
            int retVal = Externs.GetRunningObjectTable(0, out rot);

            if (retVal == 0)
            {
                rot.EnumRunning(out enumMoniker);

                var fetched = IntPtr.Zero;
                var moniker = new IMoniker[1];
                while (enumMoniker.Next(1, moniker, fetched) == 0)
                {
                    IBindCtx bindCtx;
                    Externs.CreateBindCtx(0, out bindCtx);
                    string displayName;
                    moniker[0].GetDisplayName(bindCtx, null, out displayName);
                    if (displayName.StartsWith("!VisualStudio.DTE."))
                    {
                        object ret;
                        rot.GetObject(moniker[0], out ret);
                        yield return (dynamic)ret;
                    }
                }
            }
        }

        static class Externs
        {
            [DllImport("ole32.dll")]
            public static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);

            [DllImport("ole32.dll")]
            public static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);
        }

        [ComImport, Guid("00000016-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IOleMessageFilter
        {
            [PreserveSig]
            int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo);

            [PreserveSig]
            int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);

            [PreserveSig]
            int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
        }

        class MessageFilter : IOleMessageFilter
        {
            private const int Handled = 0, RetryAllowed = 2, Retry = 99, Cancel = -1, WaitAndDispatch = 2;

            int IOleMessageFilter.HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo)
            {
                return Handled;
            }

            int IOleMessageFilter.RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
            {
                return dwRejectType == RetryAllowed ? Retry : Cancel;
            }

            int IOleMessageFilter.MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
            {
                return WaitAndDispatch;
            }

            public static void Register()
            {
                CoRegisterMessageFilter(new MessageFilter());
            }

            public static void Revoke()
            {
                CoRegisterMessageFilter(null);
            }

            private static void CoRegisterMessageFilter(IOleMessageFilter newFilter)
            {
                IOleMessageFilter oldFilter;
                CoRegisterMessageFilter(newFilter, out oldFilter);
            }

            [DllImport("Ole32.dll")]
            private static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out IOleMessageFilter oldFilter);
        }

        static IEnumerable<Test> QueueTests(Type @class, bool suppressCoverage)
        {
            var pre = @class.GetMethods().SingleOrDefault(m => m.GetCustomAttribute<TestInitializeAttribute>() != null);
            var post = @class.GetMethods().SingleOrDefault(m => m.GetCustomAttribute<TestCleanupAttribute>() != null);

            var inst = Activator.CreateInstance(@class);

            foreach (var mtd in @class.GetMethods().Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null))
            {
                var testName = @class.Name + "." + mtd.Name;

                RunTestDelegate run =
                    (out List<Covered> coverage, out string errorMessage) =>
                    {
                        Task<List<Covered>> coverageTask = null;
                        if (!suppressCoverage)
                        {
                            coverageTask = CollectCoverageAsync(testName);
                        }

                        var sw = new Stopwatch();
                        Exception failed;
                        Console.Write($"Running {testName} ");
                        sw.Start();
                        try
                        {
                            pre?.Invoke(inst, Empty);
                            mtd.Invoke(inst, Empty);
                            post?.Invoke(inst, Empty);
                            failed = null;
                        }
                        catch (Exception e)
                        {
                            failed = e;
                        }

                        if (coverageTask != null)
                        {
                            if (failed == null)
                            {
                                coverage = coverageTask.Result;
                            }
                            else
                            {
                                coverage = null;
                            }
                        }
                        else
                        {
                            coverage = null;
                        }
                        sw.Stop();

                        long workingSet;
                        using (var proc = Process.GetCurrentProcess())
                        {
                            workingSet = proc.WorkingSet64;
                        }
                        
                        Console.Write($" ({sw.ElapsedMilliseconds:N0}ms, {workingSet:N0} bytes)... ");

                        if (failed == null)
                        {
                            Console.WriteLine("Passed");
                            errorMessage = null;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("!!FAILED!!");
                            var exc = GetException(failed, 1);
                            Console.WriteLine(exc);
                            errorMessage = exc;
                            return false;
                        }
                    };

                yield return new Test { Name = testName, Run = run };
            }
        }

        static string GetException(Exception e, int depth)
        {
            var prefix = string.Join("", Enumerable.Repeat("  ", depth));
            var basic = e.Message + "\r\n" + e.StackTrace;

            string final;
            if (e.InnerException == null)
            {
                final = basic;
            }
            else
            {
                var next = GetException(e.InnerException, depth + 1);
                final = basic + "\r\n" + next;
            }

            var indented = Regex.Replace(final, "^", prefix, RegexOptions.Multiline);
            return indented;
        }
    }

    static class Extern
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        public enum JobObjectInfoType
        {
            AssociateCompletionPortInformation = 7,
            BasicLimitInformation = 2,
            BasicUIRestrictions = 4,
            EndOfJobTimeInformation = 6,
            ExtendedLimitInformation = 9,
            SecurityLimitInformation = 5,
            GroupInformation = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public Int64 PerProcessUserTimeLimit;
            public Int64 PerJobUserTimeLimit;
            public Int16 LimitFlags;
            public UInt32 MinimumWorkingSetSize;
            public UInt32 MaximumWorkingSetSize;
            public Int16 ActiveProcessLimit;
            public Int64 Affinity;
            public Int16 PriorityClass;
            public Int16 SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IO_COUNTERS
        {
            public UInt64 ReadOperationCount;
            public UInt64 WriteOperationCount;
            public UInt64 OtherOperationCount;
            public UInt64 ReadTransferCount;
            public UInt64 WriteTransferCount;
            public UInt64 OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public UInt32 ProcessMemoryLimit;
            public UInt32 JobMemoryLimit;
            public UInt32 PeakProcessMemoryUsed;
            public UInt32 PeakJobMemoryUsed;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateJobObject([In] ref SECURITY_ATTRIBUTES lpJobAttributes, string lpName);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);

        [DllImport("kernel32.dll")]
        public static extern bool SetInformationJobObject(IntPtr hJob, JobObjectInfoType infoType, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);
    }
}