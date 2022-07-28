using SeaHopper.Compiler.Pattern;
using static SeaHopper.Compiler.Lexer.StreamLexer;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Block_State : Minecraft
                {
                    // TODO: Implement Contraint checks for state type to be contained in block type
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            Patterns.Literals.NameLiteral,
                            Patterns.Operators["="],
                            Patterns.Literals.NameLiteral
                        });
                }
            }
        }
    }
}
