using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using MCDatapackCompiler.Properties;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public class TreeHead : GeneralContext
        {
            public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, prefix) => {
                    string str = "";

                    string packName = "DirtScript";
                    Directory.CreateDirectory(packName);
                    File.WriteAllText(packName + "/pack.mcmeta", Resources.pack_mcmeta);

                    foreach (var expression in expressions)
                    {
                        str = str + expression.Build(packName) + "\n";
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
