using System;

namespace Template.Generator.Core.Models
{
    public class Package
    {
        public string Name { get; }
        public string Version { get; }
        public Package(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}