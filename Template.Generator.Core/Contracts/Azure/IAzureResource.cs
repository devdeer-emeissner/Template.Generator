using Template.Generator.Core.Types;

namespace Template.Generator.Core.Contracts
{
    using System.Collections.Generic;

    public interface IAzureResource
    {
        string Id { get; set; }
        string Name { get; set; }
        IEnumerable<string> Args { get; set; }
    }
}