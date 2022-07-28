using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Builder.Context.Global
{
    public class Namespace
    {
        public string Identifier { get; private set; }

        // Functions inherent to this namespace
        Dictionary<string, Function> functions = new();
        public IReadOnlyDictionary<string, Function> Functions { get => functions; }


        // All explicit function tags for this namespace
        Dictionary<string, TagList> functionTags = new();
        public IReadOnlyDictionary<string, TagList> FunctionTags { get => functionTags; }


        public Namespace(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }



        public void AddFunction(Function function)
        {
            if (function.Parent != null) throw new Exception("Function already assigned");
            if (functions.ContainsKey(function.Identifier)) throw new Exception("function '" + function.Identifier + "' already defined for namespace '"+ this.Identifier +"'");
            function.Parent = this;
            functions.Add(function.Identifier, function);
        }

        public void AddTaggedFunction(string tagName, Function function)
        {
            if (functionTags.ContainsKey(tagName))
                functionTags[tagName].AddFunction(function);
            else
            {
                var tagList = new TagList(tagName);
                tagList.AddFunction(function);

                functionTags[tagName] = tagList;
            }
        }
    }
}
