using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Anchor : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            Patterns.Keywords["eyes"],
                            Patterns.Keywords["feet"]
                        });
                }
            }
        }
    }
}
