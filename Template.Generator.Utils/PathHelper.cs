using System;

namespace Template.Generator.Utils
{
    public static class PathHelper
    {
        public static string GetProjectRootPath()
        {
            var appRoot = AppContext.BaseDirectory.Substring(0,AppContext.BaseDirectory.LastIndexOf("/bin", StringComparison.Ordinal));
            return appRoot.Substring(0,appRoot.LastIndexOf("/", StringComparison.Ordinal)+1);
        }
    }
}