using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Lexer;
using System.Text;

namespace SeaHopper.Compiler.Trees.Expressions
{
    public class StatementHolder : Expression
    {
        readonly IReadOnlyList<IBuildable> children;
        public IReadOnlyList<IBuildable> Children => children;

        public StatementHolder(IReadOnlyList<IBuildable> children, Func<IReadOnlyList<IBuildable>, Builder.Context.BuildContext, string> printer) : base(printer)
        {
            this.children = children;
        }

        public StatementHolder(IReadOnlyList<IBuildable> children) : 
            base(
                (expressions, context) => 
                {
                    string prefix = null;
                    if (context != null) prefix = context.Data["prefix"];
                    if (context == null) context = new Builder.Context.BuildContext("");
                    context.Data["prefix"] = null;

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
                        builder.Append(expr.Build(context));
                    }

                    context.Data["prefix"] = prefix;
                    return builder.ToString();
                })
        {
            this.children = children ?? throw new ArgumentNullException(nameof(children));
        }

        public override string Build() => printer(children, null);
        public override string Build(Builder.Context.BuildContext context) => printer(children, context);
    }
}
