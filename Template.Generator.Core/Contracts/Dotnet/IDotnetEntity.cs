using System;
using System.Collections.Generic;
using Template.Generator.Core.Types;

namespace Template.Generator.Core.Contracts
{
    public interface IDotnetEntity
    {
        string Guid { get; }
        IEnumerable<string> Args { get; set; }
    }
}