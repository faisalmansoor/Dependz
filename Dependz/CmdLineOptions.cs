using System;
using System.Collections.Generic;
using NDesk.Options;

namespace Dependz
{
    class CmdLineOptions
    {
        public bool ShowHelp { get; private set; }

        public bool Verbose { get; private set; }

        public string ProcmonLogFilePath { get; private set; }

        public string ProcessName { get; private set; }

        public CmdLineOptions(IEnumerable<string> args)
        {
            var options = new OptionSet
                              {
                                  {
                                      "i=|input=", "Path of procmon event log.",
                                      v => ProcmonLogFilePath = v
                                      },
                                  {
                                      "p=|process=", "Process Name to analyze",
                                      v =>ProcessName = v
                                  },
                                  {
                                      "v", "Display all dependency, by default only failed dependency are displayed.",
                                      v => 
                                      { 
                                          if (v != null)
                                          {
                                              Verbose = true;
                                          } 
                                      }
                                  },
                                  {
                                      "h|help", "show this message and exit",
                                      v => ShowHelp = v != null
                                  }
                              };

            options.Parse(args);

            if (ShowHelp)
            {
                PrintHelp(options);
                return;
            }

            if (string.IsNullOrWhiteSpace(ProcmonLogFilePath))
            {
                throw new OptionException("Process monitor log file not provided.", "i");
            }

            if (string.IsNullOrWhiteSpace(ProcessName))
            {
                throw new OptionException("Process name is not provided.", "p|process");
            }
        }

        static void PrintHelp(OptionSet p)
        {
            Console.WriteLine("Usage: Dependz [OPTIONS]+");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        public static CmdLineOptions Create(string[] args)
        {
            CmdLineOptions options = null;
            try
            {
                options = new CmdLineOptions(args);
                if(options.ShowHelp)
                {
                    return null;
                }
            }
            catch (OptionException e)
            {
                Console.Write("Dependz: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `Dependz --help' for more information.");
                return null;
            }

            return options;
        }
    }
}
