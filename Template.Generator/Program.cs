using System;
using Template.Generator.Cli;
using Newtonsoft.Json;
using Template.Generator.Core.Models;

namespace Template.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var temp = Guid.NewGuid();
            var json = JsonConvert.SerializeObject(temp);
            var guid = JsonConvert.DeserializeObject<Guid>(json); 
        }
    }
}
