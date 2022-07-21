using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDatapackCompiler.Compiler.Builder.Context.Global
{
    public class Datapack
    {
        /// <summary>
        /// Gets or creates a namespace under the referenced name
        /// </summary>
        /// <remarks>
        /// Any and all namespaces created through this will get built into folders.
        /// Make sure to not test for them using this function.
        /// </remarks>
        /// <param name="name">Name of Namespace</param>
        /// <returns>Existing or Created Namespace under Name</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Namespace this[string name] { 
            get {
                if (name == null) throw new ArgumentNullException("name");

                if (namespaces.TryGetValue(name, out Namespace? ns))
                {
                    return ns;
                }
                else
                {
                    ns = new Namespace(name);
                    namespaces.Add(name, ns);
                    return ns;
                }
            } 
        } 


        /// <summary>
        /// Name found in both pack.mcmeta as well as folder name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description found in pack.mcmeta
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Minecraft Target Version - changes with compilation target
        /// </summary>
        public int Version { get; set; }

        private bool built = false;

        readonly Dictionary<string, Namespace> namespaces = new Dictionary<string, Namespace>();
        public Dictionary<string, Namespace> Namespace { get => namespaces; }

        public Datapack()
        {
            // Default Values
            // TODO: Load these from resource file
            Name = "SeaHopper";
            Description = "SeaHopper generated Datapack";
            Version = 10; 
        }

        /// <summary>
        /// Completes building the Functions
        /// </summary>
        /// <param name="context"></param>
        public void Build(BuildContext context)
        {
            if (built) throw new Exception("Already built");

            foreach (var nmspc in namespaces)
            {
                foreach (var fun in nmspc.Value.Functions.Values)
                {
                    fun.Build(context);
                }
            }

            built = true;
        }

        /// <summary>
        /// Writes the Datapack to File
        /// </summary>
        /// <param name="targetDir"></param>
        /// <exception cref="Exception"></exception>
        public DirectoryInfo WriteToFile(string targetDir)
        {
            if (!built) throw new Exception("Not yet built");

            // Creating and writing pack.mcmeta for datapack
            string packMcMeta = "{\"pack\":{\"pack_format\":" + Version + ",\"description\":\"" + Description + "\"}}";
            string packDir = targetDir + "/" + Name;

            var dir = Directory.CreateDirectory(packDir);
            File.WriteAllText(packDir + "/pack.mcmeta", packMcMeta);

            foreach (var @namespace in namespaces.Values)
            {
                // Creating namespace directory & its functions directory
                string namespaceDir = targetDir + "/" + Name + "/data/" + @namespace.Identifier;
                string functionsDir = namespaceDir + "/functions";
                Directory.CreateDirectory(functionsDir);

                // Writing functions to file
                foreach (var function in @namespace.Functions.Values)
                {
                    string fileTarget = functionsDir + "/" + function.Identifier + ".mcfunction";
                    File.WriteAllText(fileTarget, function.Body);
                }

                // Creating namespace's function tag directory
                string functionTagsDir = namespaceDir + "/tags/functions";
                Directory.CreateDirectory(functionTagsDir);

                // Writing tag libraries to file
                foreach (var tagList in @namespace.FunctionTags.Values)
                {
                    string file = "{\"values\":[\n";
                    foreach (var item in tagList.functions.Values)
                    {
                        if (item.Parent == null) throw new Exception("Cannot tag function with no related parent");
                        file += "\t\"" + item.Parent.Identifier + ":" + item.Identifier + "\"\n";
                    }

                    file += "]}";
                    File.WriteAllText(functionTagsDir + "/"  + tagList.Identifier + ".json", file);
                }
            }

            return dir;
        }
    }
}
