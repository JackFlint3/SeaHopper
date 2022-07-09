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

        public NamespaceHolder(Func<IReadOnlyList<IExpression>, string, string> printer) : base(printer)
        {
        }

        internal void RegisterFunctionTag(string tag, string function)
        {

        }
    }
}
