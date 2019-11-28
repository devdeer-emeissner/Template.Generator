using Template.Generator.Core.Types;

namespace Template.Generator.Core.Contracts
{
    public interface IAzureResource
    {
        string Id { get; set; }
        string Name { get; set; }
        string[] Args { get; set; }
    }
}