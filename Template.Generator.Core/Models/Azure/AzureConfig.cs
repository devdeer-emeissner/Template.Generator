using System.Collections.Generic;
using Template.Generator.Core.Contracts;

namespace Template.Generator.Core.Models.Azure
{
    public class AzureConfig
    {
        public List<AzureResource> Resources { get; set; }
    }
}