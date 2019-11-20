using System;
using System.Collections.Generic;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;
using Template.Generator.Utils;

namespace Template.Generator.Core.Models
{
    public class DotnetSolution : IDotnetEntity
    {
        public string Guid { get; }
        public string Path { get; set; } = PathHelper.GetProjectRootPath();
        public string Name { get; set; }
        public IList<string> ProjectRefs { get; } = new List<string>();
        public IEnumerable<string> Args { get; set; } = new List<string>();
    }
}