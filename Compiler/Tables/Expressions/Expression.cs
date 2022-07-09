using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public abstract class Expression : IExpression
    {
        protected internal readonly Func<IReadOnlyList<IExpression>,string, string> printer;

        protected Expression(Func<IReadOnlyList<IExpression>,string, string> printer)
        {
            this.printer = printer;
        }

        public virtual string Build() => printer(Array.Empty<IExpression>(),"");
        public virtual string Build(string prefix) => printer(Array.Empty<IExpression>(), prefix);
    }
}
