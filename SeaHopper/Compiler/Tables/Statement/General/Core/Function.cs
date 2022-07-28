using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;
using SeaHopper.Compiler.Pattern;
using SeaHopper.Compiler.Trees.Expressions;
using SeaHopper.Compiler.Builder;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public class Function : Unspecific
        {
            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, context) => {
                    string str = "";
                    if (expressions.Count == 0) return str;

                    int iExpr = expressions.Count - 1;
                    var expr = expressions[iExpr];
                    str += expr.Build(context) + "\r\n";
                    
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
                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, prefix) => "");
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new()
                    {
                        Patterns.Symbols["["],
                        Patterns.One(new()
                        {
                            Patterns.All(new()
                            {
                                Patterns.Literals.NameLiteral,
                                Patterns.Operators[":"],
                                Patterns.Literals.NameLiteral
                            }),
                            Patterns.Literals.NameLiteral
                        }),
                        Patterns.Symbols["]"]
                    });
            }
        }


    }
}
