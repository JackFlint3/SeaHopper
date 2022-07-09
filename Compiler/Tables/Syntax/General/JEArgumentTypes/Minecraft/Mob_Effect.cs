using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Mob_Effect : Minecraft
                {
                    // Constraint: Effect must be ID
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            Patterns.Literals.NameLiteral
                        });
                }
            }
        }
    }
}
