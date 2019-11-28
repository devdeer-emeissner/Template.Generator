using Template.Generator.Core.Models.Azure;
using System;
namespace Template.Generator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;

    using Cli;

    using Core.Models;

    public static class AzureProcessor
    {
        public static void ProcessConfig(AzureConfig cfg)
        {
            if (cfg.Resources != null)
            {
                if (Azure.Login().ForwardStdOut().ForwardStdErr().Execute().ExitCode == 0)
                {
                    //cfg.Resources.ForEach(resource => Console.WriteLine(resource.GetType().ToString()));
                    cfg.Resources.ForEach(RunCommand);
                }
            }
        }

        private static void RunCommand(AzureResource resource)
        {
            var args = new List<string>
            {
                "--name",
                resource.Name
            };
            resource.Args = resource.Args.Concat(args);
            Azure.Command(resource.Args.ToArray()).ForwardStdOut().ForwardStdErr().Execute();
        }
    }

}