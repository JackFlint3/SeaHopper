using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        /// <summary>
        /// A function body
        /// </summary>
        public class Body : GeneralContext
        {
            public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, prefix) => {
                    string str = "";
                    if (expressions.Count == 0) return str;
                    else if (expressions.Count == 1)
                        return expressions[0].Build(prefix);
                    

                    int iExpr = 1;
                    for (; iExpr < expressions.Count - 1; iExpr++)
                    {
                        var expr = expressions[iExpr];
                        str += expr.Build(prefix);
                        if (!str.EndsWith("\n")) str += "\n";
                    }
                    return str;
                });
                return holder;
            }

            public override Pattern<LexerToken> Pattern =>
                Pattern = Patterns.One(new()
                    {
                        RetrieveByType(typeof(Command)),
                        Patterns.One(new()
                        {
                            Patterns.All(new()
                            {
                                Patterns.Symbols["{"],
                                Patterns.Symbols["}"]
                            }),
                            Patterns.All(new() {
                                Patterns.Symbols["{"],
                                Patterns.One(new()
                                {
                                    Patterns.All(new()
                                    {
                                        Patterns.Many(RetrieveByType(typeof(Command)))
                                    }),
                                    RetrieveByType(typeof(Body)),
                                }),
                                Patterns.Symbols["}"],
                            })
                        })
                    });

        }
    }
}
