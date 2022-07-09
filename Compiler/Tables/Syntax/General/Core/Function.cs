using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public class Function : GeneralContext
        {
            public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, prefix) => {
                    string str = "";
                    if (expressions.Count == 0) return str;

                    int iExpr = expressions.Count - 1;
                    var expr = expressions[iExpr];
                    str += expr.Build(prefix) + "\r\n";
                    
                    return str;
                });
                return holder;
            }
            public override Pattern<LexerToken> Pattern =>
                Patterns.One(new()
                {
                    Patterns.All(new()
                    {
                        Patterns.Many(RetrieveByType(typeof(Attribute))),
                        Patterns.Keywords["function"],
                        Patterns.Literals.NameLiteral,
                        RetrieveByType(typeof(Body))
                    }),
                    Patterns.All(new()
                    {
                        Patterns.Keywords["function"],
                        Patterns.Literals.NameLiteral,
                        RetrieveByType(typeof(Body))
                    })
                });

            public class Attribute : Function
            {
                public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, prefix) => "");
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new()
                    {
                        Patterns.Symbols["["],
                        Patterns.Literals.NameLiteral,
                        Patterns.Symbols["]"]
                    });
            }
        }


    }
}
