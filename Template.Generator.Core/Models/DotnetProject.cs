using System;
using System.Collections.Generic;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Models
{
    public class DotnetProject : IDotnetEntity
    {
        public Guid Id { get; set; }
        public EntityType Type { get; set; }
        public string Name { get; set; }
        public IList<Guid> ProjectRefs { get; set; }
        public IList<Package> Packages { get; set; }
        public string[] Args { get; set; }
    }
}