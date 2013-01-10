using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProcMonLogParser;

namespace Dependz
{
    class Program
    {
        public static int Main(string[] args)
        {
            CmdLineOptions options = CmdLineOptions.Create(args);
            if (options == null)
            {
                return -1;
            }

            if(!File.Exists(options.ProcmonLogFilePath))
            {
                Console.WriteLine("Can not find procmon log file {0}", options.ProcmonLogFilePath);
                return -2;
            }

            var parser = new PmParser(options.ProcmonLogFilePath);

            var dependecies = from evnt in parser.GetEvents()
                         where String.Compare(evnt.Process_Name, options.ProcessName, true) == 0 &&
                               (evnt.Operation == PmOperations.CreateFile ||
                               evnt.Operation == PmOperations.LoadImage ||
                                evnt.Operation == PmOperations.CreateFileMapping) &&
                               String.Compare(evnt.PathExt, ".dll", true) == 0
                         group evnt by evnt.PathFileName.ToUpper();
            
            if(dependecies.Count() == 0)
            {
                Console.WriteLine("Didn't found any dependencies for process {0} in log file {1}", options.ProcessName, options.ProcmonLogFilePath);
                return -3;
            }

            Report(options, dependecies.Select(Analyze));

            return 0;
        }

        private static Dependency Analyze(IEnumerable<PmEvent> dependency)
        {
            var pmDependency = new Dependency();
            foreach (PmEvent pmEvent in dependency)
            {
                switch (pmEvent.Result)
                {
                    case PmResults.NAME_NOT_FOUND:
                    case PmResults.PATH_NOT_FOUND:
                        pmDependency.AddPath(pmEvent.Path, DependencyStatus.NotFound);
                        break;
                    case PmResults.ACCESS_DENIED:
                        pmDependency.AddPath(pmEvent.Path, DependencyStatus.Resolved, true);
                        break;
                    case PmResults.SUCCESS:
                    case PmResults.FILE_LOCKED_WITH_ONLY_READERS:
                        pmDependency.AddPath(pmEvent.Path, DependencyStatus.Resolved);
                        break;
                }
            }
            return pmDependency;
        }

        private static void Report(CmdLineOptions options, IEnumerable<Dependency> dependencies)
        {
            Console.WriteLine("Process: {0}", options.ProcessName);
            if(!options.Verbose)
            {
                List<Dependency> unresolved = dependencies.Where( d => d.Status != DependencyStatus.Resolved || d.AccessDenied).ToList();
                if(unresolved.Count() == 0)
                {
                    Console.WriteLine("No Unresolved Dependencies found.");
                }
                else
                {
                    PrintDependencies(unresolved);
                }
            }
            else
            {
                PrintDependencies(dependencies);
            }
        }

        private static void PrintDependencies(IEnumerable<Dependency> dependencies)
        {
            foreach (Dependency dependency in dependencies)
            {
                Console.Write("{0} - {1}: ", dependency.Name, dependency.AccessDenied ? "AccessDenied" : dependency.Status.ToString());
                
                if (dependency.Status == DependencyStatus.Resolved)
                {
                    Console.Write("{0}", dependency.ResolvedPath );
                }

                Console.WriteLine();
                Console.WriteLine("Probed Paths:");
                foreach (var probedPath in dependency.ProbedPaths)
                {
                    Console.WriteLine("\t{0}", probedPath);
                }
                Console.WriteLine();
            }
        }
    }
}
