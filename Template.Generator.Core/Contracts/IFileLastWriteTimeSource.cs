using System;

namespace Template.Generator.Core.Contracts
{
    public interface IFileLastWriteTimeSource
    {
        DateTime GetLastWriteTimeUtc(string file);
    }
}