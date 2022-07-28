using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Trees.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Tables.Expressions.Core
{
    internal class FunctionHolder : Expression
    {
        public FunctionHolder(Func<IReadOnlyList<IBuildable>, Builder.Context.BuildContext, string> printer) : base(printer)
        {
        }
    }
}
