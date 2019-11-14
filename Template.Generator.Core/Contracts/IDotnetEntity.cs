using System;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Contracts
{
    public interface IDotnetEntity
    {
        Guid Id { get; set; }
        EntityType Type { get; set; }
        string[] Args { get; set; }
    }
}