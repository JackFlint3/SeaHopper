using MCDatapackCompiler.Compiler.Builder.Context.Global;
using MCDatapackCompiler.Compiler.Builder.Context.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDatapackCompiler.Compiler.Builder.Context
{
    public class BuildContext
    {
        public BuildContext(string destination)
        {
            Datapack = new Datapack();
            // Default namespace, always exists
            // TODO: Add objects and tags from default namespace
            Datapack.Namespace["minecraft"] = new Namespace("minecraft"); 
            CurrentFile = null;
            UsingDirectives = new List<UsingDirectives>();
            Destination = destination;
            Data = new Dictionary<string, string>();
        }

        public Datapack Datapack { get; private set; }

        public string? CurrentFile { get; set; }
        public string Destination { get; private set; }
        public Namespace? CurrentNamespace { get; set; }
        public Function? CurrentFunction { get; set; }
        public List<UsingDirectives> UsingDirectives { get; private set; }
        public Dictionary<string, string> Data { get; set; }
    }
}
