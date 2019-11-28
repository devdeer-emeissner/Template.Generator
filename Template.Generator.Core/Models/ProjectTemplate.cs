using System.Collections.Generic;
using Template.Generator.Core.Models.Dotnet;
using Template.Generator.Core.Models.Azure;

namespace Template.Generator.Core.Models
{
    public class ProjectTemplate
    {
        public string ProjectName { get; set; }
        public DotnetConfig DotnetConfig { get; set; }
        public AzureConfig AzureConfig { get; set; }
    }
}