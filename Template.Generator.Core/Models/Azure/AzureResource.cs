using System;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Models
{
    using System.Collections.Generic;

    public class AzureResource : IAzureResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Args { get; set; }
    }
}