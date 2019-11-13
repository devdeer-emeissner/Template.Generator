using System;
using System.Collections.Generic;

namespace Template.Generator.Core.Model
{
    public class DotnetConfig
    {
        
        public readonly Guid Guid = Guid.NewGuid();
        public string Name { get; set; }
        public string Location { get; set; }
        public string ShortName { get; set; }
        public IList<Param> Args = new List<Param>();
        
        public DotnetConfig()
        {
            
        }
    }
}