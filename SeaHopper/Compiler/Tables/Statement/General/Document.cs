using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;
using SeaHopper.Compiler.Pattern;
using SeaHopper.Compiler.Trees.Expressions;
using SeaHopper.Properties;
using SeaHopper.Compiler.Builder;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public class Document : Unspecific
        {
            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, context) => {
                    string str = "";

                    foreach (var expression in expressions)
                    {
                        str = str + expression.Build(context) + "\n";
                    }
                    return str;
                });
                return holder;
            }

            public override Pattern<LexerToken> Pattern =>
                Pattern = Patterns.One(new() 
                    {
                        Patterns.All(new(){
                            Patterns.Many(RetrieveByType(typeof(Directive))),
                            Patterns.Many(RetrieveByType(typeof(Namespace)))
                        }),
                        Patterns.All(new(){
                            Patterns.Many(RetrieveByType(typeof(Namespace)))
                        }),
                        // TODO: Add Patterns.Empty here - empty file is technically valid
                    });
        }


    }
}
