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
                cfg.Projects.ForEach(project => CreateProject(project));
                //sets refs in solution
                AddProjectsToSolutionReference(cfg.Solution, cfg.Projects.Select(project => $"{project.Path}{project.Name}/").ToList());
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
            Dotnet.New("sln", solution.Args.ToArray())
                    .ForwardStdOut()
                    .ForwardStdErr()
                    .Execute();
        }

        private static void CreateProject(DotnetProject project)
        {
            var args = new List<string> {
                 "--name", project.Name,
                 "--output", PathHelper.GetProjectRootPath() + $"/{project.Name}"
            };
            project.Args = project.Args.Concat(args);
            Dotnet.New(project.Alias, project.Args.ToArray())
                    .ForwardStdErr()
                    .ForwardStdOut()
                    .Execute();
        }

        private static DotnetProject GetProject(IList<DotnetProject> projects, string guid)
        {
            return projects.Single(x => x.Guid == guid);
        }

        private static void AddProjectsToSolutionReference(DotnetSolution solution, IReadOnlyList<string> projectPaths)
        {
            Dotnet.AddProjectsToSolution(solution.Path, projectPaths)
                    .ForwardStdOut()
                    .ForwardStdErr()
                    .Execute();
        }

        private static void AddProjectReference(DotnetProject project, DotnetProject refProject)
        {
            Dotnet.AddProjectToProjectReference(project.Path + project.Name, refProject.Path + refProject.Name)
                    .ForwardStdOut()
                    .ForwardStdErr()
                    .Execute();
        }

       private static void AddPackageReference(DotnetProject proj, Package package)
       {

       }
    }
}