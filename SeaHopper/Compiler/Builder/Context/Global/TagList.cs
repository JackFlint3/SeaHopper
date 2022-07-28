using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Builder.Context.Global
{
    public class TagList
    {
        public string Identifier { get; private set; }
        public Dictionary<string, Function> functions = new Dictionary<string, Function>();

        public TagList(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }

        public void AddFunction(Function function)
        {
            if (function.Parent == null) throw new Exception("Cannot link unassigned function '" + function.Identifier + "' to taglist '" + Identifier + "'");
            string funKey = function.Parent.Identifier + ":" + function.Identifier;
            if (functions.ContainsKey(funKey)) throw new Exception("A function of '" + funKey + "' already exists");
            functions[funKey] = function;
        }
    }
}
