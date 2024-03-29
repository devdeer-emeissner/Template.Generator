using System;
using System.Collections.Generic;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;
using Template.Generator.Utils;

namespace Template.Generator.Core.Models.Dotnet
{
    public class DotnetProject : IDotnetEntity
    {
        public string Guid { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Path { get; set; } = PathHelper.GetProjectRootPath();
        public List<string> ProjectRefs { get; set; } = new List<string>();
        public List<Package> Packages { get; set; } = new List<Package>();
        public IEnumerable<string> Args { get; set; } = new List<string>();
    }
}