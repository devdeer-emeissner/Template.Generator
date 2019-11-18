using System.Collections.Generic;

namespace Template.Generator.Core.Models
{
    public class DotnetConfig
    {
        public DotnetSolution Solution { get; set; }
        public IList<DotnetProject> Projects { get; set; } = new List<DotnetProject>();
        public string RootPath { get; set; }
    }
}