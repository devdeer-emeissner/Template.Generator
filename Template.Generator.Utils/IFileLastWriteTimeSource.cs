using System;

namespace Template.Generator.Utils
{
    public interface IFileLastWriteTimeSource
    {
        DateTime GetLastWriteTimeUtc(string file);
    }
}