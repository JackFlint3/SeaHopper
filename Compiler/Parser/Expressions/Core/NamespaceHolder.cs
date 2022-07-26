using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDatapackCompiler.Compiler.Tables.Expressions.Core
{
    internal class NamespaceHolder : Expression
    {

        Dictionary<string, List<string>> functionRegistry = new();

        public NamespaceHolder(Func<IReadOnlyList<IBuildable>, Builder.Context.BuildContext, string> printer) : base(printer)
        {
        }
    }
}