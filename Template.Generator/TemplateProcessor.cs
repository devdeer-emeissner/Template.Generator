using System;
using System.Linq;
using Newtonsoft.Json;
using Template.Generator.Core.Models;


namespace Template.Generator
{
    using Utils;

    public class TemplateProcessor
    {
        private string _rootPath;
        private IPhysicalFileSystem _fileSystem { get; set; }
        private ProjectTemplate _projectTemplate;
        private string _projectTemplateJson;
        private const string _templateSearchPattern = "*_template.config";
        public string ProjectTemplateJson 
        {
            get 
            {
                if (_projectTemplate == null)
                {
                    _projectTemplate = InitProjectTemplateFromJsonConfig();
                    _projectTemplateJson = JsonConvert.SerializeObject(_projectTemplate);
                    return _projectTemplateJson;
                }
                return _projectTemplateJson;
            }
        }

        public TemplateProcessor(string rootPath)
        {
            _rootPath = rootPath;
            _fileSystem = new PhysicalFileSystem();
            _projectTemplate = InitProjectTemplateFromJsonConfig();
            ProcessTemplate();
        }

        private void ProcessTemplate()
        {
            foreach (var config in _projectTemplate.DotnetConfigs)
            {
                DotnetConfigProcessor.ProcessConfig(config);
            }
        }

        private ProjectTemplate InitProjectTemplateFromJsonConfig()
        {
            var files = FileFindHelpers.FindFilesAtOrAbovePath(_fileSystem, _rootPath, _templateSearchPattern);
            if (files.Count > 1) throw new ArgumentException("Multiple config files found, make sure that there is only one *_template.config file inside your project directory");
            Console.WriteLine(_fileSystem.ReadAllText(files.First()));
            var template = JsonConvert.DeserializeObject<ProjectTemplate>(_fileSystem.ReadAllText(files.First()));
            return template;
        }
    }
}