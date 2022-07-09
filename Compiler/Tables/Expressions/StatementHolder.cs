using MCDatapackCompiler.Compiler.Lexer;
using System.Text;

namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public class StatementHolder : Expression
    {
        readonly IReadOnlyList<IExpression> children;
        public IReadOnlyList<IExpression> Children => children;

        public StatementHolder(IReadOnlyList<IExpression> children, Func<IReadOnlyList<IExpression>, string, string> printer) : base(printer)
        {
            this.children = children;
        }

        public StatementHolder(IReadOnlyList<IExpression> children) : 
            base(
                (expressions, prefix) => 
                {
                    StringBuilder builder = new StringBuilder();
                    bool first = true;
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        builder.Append(prefix);
                        first = false;
                    }

                    foreach (var expr in children)
                    {
                        if (first) first = false;
                        else builder.Append(' ');
                        builder.Append(expr.Build());
                    }

                    return builder.ToString();
                })
        {
            this.children = children ?? throw new ArgumentNullException(nameof(children));
        }

        public override string Build() => printer(children, "");
        public override string Build(string prefix) => printer(children, prefix);
    }
}
