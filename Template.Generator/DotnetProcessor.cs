using System;
using System.Collections.Generic;
using System.Linq;
using Template.Generator.Cli;
using Template.Generator.Core.Models;
using Template.Generator.Utils;

namespace Template.Generator
{
    public static class DotnetConfigProcessor
    {
       
       public static void ProcessConfig(DotnetConfig cfg)
       {
            if (cfg.Solution != null) 
                CreateSolution(cfg.Solution);
            if (cfg.Projects != null)
            {
                //create projects
                foreach (var project in cfg.Projects)
                {
                    CreateProject(project);
                }
                //sets refs in projects
                foreach(var project in cfg.Projects)
                {
                    foreach (var reference in project.ProjectRefs)
                    {
                        var refProject = GetProject(cfg.Projects,reference);
                        if(refProject != null)
                        {
                            AddProjectReference(project, refProject);
                        }
                    }
                }
            }
       }
       
       private static void CreateSolution(DotnetSolution solution)
       {
           var args = new List<string> { 
                    "--name", solution.Name, 
                    "--output", solution.Path, 
                };
                solution.Args = solution.Args.Concat(args);
                var result = Dotnet.New("sln", solution.Args.ToArray())
                                    .CaptureStdOut()
                                    .CaptureStdErr()
                                    .Execute();
            if(!string.IsNullOrEmpty(result.StdOut) || !string.IsNullOrEmpty(result.StdErr))
                Console.Out.WriteLine(result.StdOut, result.StdErr);
       }
       
       private static void CreateProject(DotnetProject project)
       {
            var args = new List<string> {
                "--name", project.Name,
                "--output", PathHelper.GetProjectRootPath() + $"/{project.Name}"
            };
            project.Args = project.Args.Concat(args);
            var result = Dotnet.New(project.Alias, project.Args.ToArray())
                                .CaptureStdOut()
                                .CaptureStdErr()
                                .Execute();
            if(!string.IsNullOrEmpty(result.StdOut) || !string.IsNullOrEmpty(result.StdErr))
                Console.Out.WriteLine(result.StdOut, result.StdErr);
       }

       private static DotnetProject GetProject(IList<DotnetProject> projects, string guid)
       {
           return projects.Single(x => x.Guid == guid);
       }

       private static void AddProjectReference(DotnetProject project, DotnetProject refProject)
       {
            var result = Dotnet.AddProjectToProjectReference(project.Path + $"/{project.Name}.csproj", refProject.Path + $"/{refProject.Name}")
                                .CaptureStdOut()
                                .CaptureStdErr()
                                .Execute();
            if(!string.IsNullOrEmpty(result.StdOut) || !string.IsNullOrEmpty(result.StdErr))
                Console.Out.WriteLine(result.StdOut, result.StdErr);
       }

       private static void AddPackageReference(DotnetProject proj, Package package)
       {

       }
    }
}