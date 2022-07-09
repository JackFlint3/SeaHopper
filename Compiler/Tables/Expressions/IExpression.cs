using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public interface IExpression
    {
        string Build();
        string Build(string parameter);
    }
}
