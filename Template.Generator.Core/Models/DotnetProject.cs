using System;
using System.Collections.Generic;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Models
{
    public class DotnetProject : IDotnetEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public EntityType Type { get; } = EntityType.Project;
        public string Name { get; set; }
        public string Path { get; set; }
        private IList<Guid> ProjectRefs { get; set; } = new List<Guid>();
        private IList<Package> Packages { get; set; } = new List<Package>();
        public string[] Args { get; set; }

        public static void AddProjectReference(DotnetProject project, Guid id)
        {
            if (id == null || id.ToString() == string.Empty) throw new ArgumentNullException();
            if (project.ProjectRefs.Contains(id)) throw new ArgumentException("The Project already contains a reference to that project");
            project.ProjectRefs.Add(id);
        }
    }
}