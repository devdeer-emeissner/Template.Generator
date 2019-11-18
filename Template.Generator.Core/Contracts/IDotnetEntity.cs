using System;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Contracts
{
    public interface IDotnetEntity
    {
        Guid Id { get; }
        EntityType Type { get; }
        string[] Args { get; set; }
    }
}