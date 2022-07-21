using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using MCDatapackCompiler.Properties;
using MCDatapackCompiler.Compiler.Builder;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
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
