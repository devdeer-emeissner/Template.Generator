using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Template.Generator.Cli;
using Template.Generator.Core.Contracts;
using Template.Generator.Core.Models;
using Template.Generator.Core.Types;
using Template.Generator.Utils;

namespace Template.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var template = new ProjectTemplate(){
                DotnetConfigs = new List<DotnetConfig>()
                {
                    new DotnetConfig(){
                        Solution = new DotnetSolution(){ Name="TestSolution0" },
                        Projects = new List<DotnetProject>(){
                        { new DotnetProject(){ Name="TestProject0" } },
                        { new DotnetProject(){ Name="TestProject1" } },
                        { new DotnetProject(){ Name="TestProject2" } },
                        { new DotnetProject(){ Name="TestProject3" } },
                        { new DotnetProject(){ Name="TestProject4" } } 
                    }
                },
                {
                    new DotnetConfig(){
                        Solution = new DotnetSolution(){ Name="TestSolution1" },
                        Projects = new List<DotnetProject>(){
                        { new DotnetProject(){ Name="TestProject0" } },
                        { new DotnetProject(){ Name="TestProject1" } },
                        { new DotnetProject(){ Name="TestProject2" } },
                        { new DotnetProject(){ Name="TestProject3" } },
                        { new DotnetProject(){ Name="TestProject4" } } }
                    }
                },
                {
                    new DotnetConfig(){
                        Solution = new DotnetSolution(){ Name="TestSolution2" },
                        Projects = new List<DotnetProject>(){
                        { new DotnetProject(){ Name="TestProject0" } },
                        { new DotnetProject(){ Name="TestProject1" } },
                        { new DotnetProject(){ Name="TestProject2" } },
                        { new DotnetProject(){ Name="TestProject3" } },
                        { new DotnetProject(){ Name="TestProject4" } } }
                    }
                },
            }};
            var processor = new TemplateProcessor("/mnt/c/Users/EugenMeissner/Dev/Template.Generator");
            var temp = processor.ProjectTemplateJson;
            
        }
    }
}
