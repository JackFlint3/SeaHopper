using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public partial class Command
        {
            public partial class Execute : Command
            {
                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, context) => {
                        string str;
                        string prefix = null;
                        if (context != null) prefix = context.Data["prefix"];
                        if (context == null) context = new Builder.Context.BuildContext("");

                        if (string.IsNullOrEmpty(prefix))
                            str = "execute";
                        else str = prefix + " execute";

                        if (expressions.Count == 0) return str;

                        int iExpr = 0;
                        for (; iExpr < expressions.Count - 1; iExpr++)
                        {
                            var expr = expressions[iExpr];
                            str += " " + expr.Build();
                        }
                        str += " run";
                        
                        context.Data["prefix"] = str;
                        str = expressions[iExpr].Build(context);
                        context.Data["prefix"] = prefix;

                        return str;
                    });
                    return holder;
                }

                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new() {
                        Patterns.Many(RetrieveByType(typeof(Subcommand))),
                        Patterns.One(new()
                        {
                            Patterns.All(new()
                            {
                                RetrieveByType(typeof(Command)),
                                Patterns.Symbols[";"]
                            }),
                            RetrieveByType(typeof(Body))
                        })
                    });

                public partial class Subcommand : Execute
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        return new StatementHolder(expressions);
                    }

                    public override Pattern<LexerToken> Pattern =>
                        new Pattern<LexerToken>.RequiredOne(
                                GetSubclassesForType(typeof(Subcommand))
                            );
                }
            }
        }
    }
}
