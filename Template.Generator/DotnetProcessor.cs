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
                //TODO: make CreateProject() run in parallel
                //create projects
                cfg.Projects.ForEach(project => CreateProject(project));
                //adds refs to solution
                AddProjectsToSolutionReference(cfg.Solution, cfg.Projects.Select(project => $"{project.Path}{project.Name}/").ToList());
                foreach(var project in cfg.Projects)
                {
                    //adds refs to projects
                    if (project.ProjectRefs != null)
                    {
                        foreach (var refGuid in project.ProjectRefs)
                        {
                            var refProject = GetProjectByGuid(cfg.Projects,refGuid);
                            if(refProject != null)
                            {
                                AddProjectReference(project, refProject);
                            }
                        } 
                    }
                    //adds package refs to project
                    if(project.Packages != null)
                    {
                        project.Packages.ForEach(package => AddPackageReference(project, package));
                    }   
                }
                RestoreSolution(cfg.Solution);
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
                 "--output", PathHelper.GetProjectRootPath() + $"/{project.Name}",
                 "--no-restore"

            };
            project.Args = project.Args.Concat(args);
            Dotnet.New(project.Alias, project.Args.ToArray())
                    .ForwardStdErr()
                    .ForwardStdOut()
                    .Execute();
        }

        private static DotnetProject GetProjectByGuid(IList<DotnetProject> projects, string guid)
        {
            return projects.Single(x => x.Guid == guid);
        }

        private static void RestoreSolution(DotnetSolution solution)
        {
            Dotnet.Restore($"{solution.Path}{solution.Name}.sln")
                    .ForwardStdOut()
                    .ForwardStdErr()
                    .Execute();
        }

        private static void AddProjectsToSolutionReference(DotnetSolution solution, IReadOnlyList<string> projectPaths)
        {
            Dotnet.AddProjectsToSolution($"{solution.Path}{solution.Name}.sln", projectPaths)
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

       private static void AddPackageReference(DotnetProject project, Package package)
       {
           //use latest version if no version is provided
           if (package.Version == null)
           {
                Dotnet.AddPackageReference($"{project.Path}{project.Name}",package.Name)
                        .ForwardStdOut()
                        .ForwardStdErr()
                        .Execute();
           } 
           else
           {
               Dotnet.AddPackageReference($"{project.Path}{project.Name}", package.Name, package.Version)
                        .ForwardStdOut()
                        .ForwardStdErr()
                        .Execute();
           }
           
       }
    }
}