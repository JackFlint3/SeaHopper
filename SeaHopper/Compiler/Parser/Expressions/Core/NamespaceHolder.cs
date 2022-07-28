using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Trees.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Tables.Expressions.Core
{
    internal class NamespaceHolder : Expression
    {

        Dictionary<string, List<string>> functionRegistry = new();

        public NamespaceHolder(Func<IReadOnlyList<IBuildable>, Builder.Context.BuildContext, string> printer) : base(printer)
        {
        }
    }
}