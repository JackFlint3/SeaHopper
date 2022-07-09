using MCDatapackCompiler.Compiler.Trees.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDatapackCompiler.Compiler.Tables.Expressions.Core
{
    internal class FunctionHolder : Expression
    {
        public FunctionHolder(Func<IReadOnlyList<IExpression>, string, string> printer) : base(printer)
        {
        }
    }
}
