using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class ScoreHolder : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                            //RetrieveByType(typeof(UUID)),
                            Patterns.Literals.NameLiteral,
                            Patterns.Operators["*"]
                        });
                }
            }
        }
    }
}
