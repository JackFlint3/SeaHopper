using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Trees.Expressions;
using SeaHopper.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Tables.Expressions.Core
{
    internal class DatapackHolder : Expression
    {
        string dataPackName;
        readonly Dictionary<string, NamespaceHolder> _namespaces = new Dictionary<string, NamespaceHolder>();
        public DatapackHolder(IReadOnlyList<IBuildable> _namespaces, string name) : base((_,_) => "")
        {
            this.dataPackName = name;
        }

        internal IReadOnlyDictionary<string, NamespaceHolder> Namespaces => _namespaces;

        public string DataPackName { get => dataPackName; set => dataPackName = value; }

        internal void RegisterNamespace(NamespaceHolder holder)
        {

        }

        public override string Build()
        {
            Directory.CreateDirectory(DataPackName);
            File.WriteAllText(DataPackName + "/pack.mcmeta", Resources.pack_mcmeta);
            return base.Build();
        }

        public override string Build(Builder.Context.BuildContext context) //Takes no arguments, prefix is ignored
        {
            return this.Build();
        }
    }
}
