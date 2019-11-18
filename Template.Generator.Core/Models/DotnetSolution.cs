using System;
using System.Collections.Generic;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Models
{
    public class DotnetSolution : IDotnetEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public EntityType Type { get; } = EntityType.Solution;
        public string Path { get; set; }
        public string Name { get; set; }
        public IList<Guid> ProjectRefs { get; }
        public string[] Args { get; set;}
    }
}