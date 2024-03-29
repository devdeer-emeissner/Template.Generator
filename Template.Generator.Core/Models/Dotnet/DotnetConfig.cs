using System.Collections.Generic;

namespace Template.Generator.Core.Models.Dotnet
{
    public class DotnetConfig
    {
        public DotnetSolution Solution { get; set; }
        public List<DotnetProject> Projects { get; set; } = new List<DotnetProject>();
        public string RootPath { get; set; }
    }
}