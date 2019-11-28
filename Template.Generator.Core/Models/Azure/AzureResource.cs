using System;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Models
{
    public class AzureResource : IAzureResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Args { get; set; }
    }
}