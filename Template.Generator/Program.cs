using System;
using System.Collections.Generic;
using System.Linq;
using Template.Generator.Cli;
using Template.Generator.Utils;

namespace Template.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new TemplateProcessor(PathHelper.GetProjectRootPath());
        }
    }
}
