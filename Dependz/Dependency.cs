using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dependz
{
    public enum DependencyStatus
    {
        Unknown,
        NotFound,
        Resolved,
        AccessDenied
    }

    public class Dependency
    {
        public readonly List<string> ProbedPaths = new List<string>();
        public string ResolvedPath { get; set; }
        public DependencyStatus Status { get; private set; }

        public void AddPath(string path, DependencyStatus newStatus)
        {
            if(!ProbedPaths.Contains(path))
            {
                ProbedPaths.Add(path);
            }

            if(newStatus == DependencyStatus.Resolved)
            {
                ResolvedPath = path;
            }

            Status = newStatus;
        }

        public string Name
        {
            get
            {
                if(!String.IsNullOrWhiteSpace(ResolvedPath))
                {
                    return Path.GetFileName(ResolvedPath);
                }

                if(ProbedPaths.Count > 0)
                {
                    return Path.GetFileName(ProbedPaths.First());
                }

                return "NA";
            }
        }
    }
}
