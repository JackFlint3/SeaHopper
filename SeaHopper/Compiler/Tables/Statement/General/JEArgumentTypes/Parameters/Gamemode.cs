using SeaHopper.Compiler.Pattern;
using static SeaHopper.Compiler.Lexer.StreamLexer;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
public abstract partial class JEArgumentTypes
        {
            public abstract partial class Parameters
            {
                public class Gamemode : JEArgumentTypes
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                        Patterns.Keywords["survival"],
                        Patterns.Keywords["creative"],
                        Patterns.Keywords["spectator"],
                        Patterns.Keywords["adventure"]
                        });
                }
            }
        }
    }
}
