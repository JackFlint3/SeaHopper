using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Lexer;
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler.Parser.Trees.Syntax
{
    public partial class StatementDiagram
    {
        public abstract class Statement : IMatchable<StreamLexer.LexerToken>, IExpressionProvider<StreamLexer.LexerToken>
        {
            public virtual Pattern<StreamLexer.LexerToken> Pattern { get; set; }
                


            public virtual bool Match(IExpressionBuilder<StreamLexer.LexerToken> builder)
            {
                builder.Prepare(this);

                if (Pattern.Match(builder))
                {

                    var expr = builder.Collapse();
                    if (expr != null) builder.Collect(expr);

                    return true;
                }
                else
                {
                    builder.Discard();
                    return false;
                }
            }

            public Statement() {
                RegisterByType(this.GetType(), this);

                if (Pattern == null) Pattern = new Pattern<StreamLexer.LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));
                else this.Pattern = Pattern;
            }

            public override string ToString()
            {
                return "<" + this.GetType().Name + ">";
            }

            public virtual Expression GetExpression(IReadOnlyList<IBuildable> expressions)
            {
                return new StatementHolder(expressions);
            }
        }
    }
}
