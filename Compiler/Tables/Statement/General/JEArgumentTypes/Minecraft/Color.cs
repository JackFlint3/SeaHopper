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
                public class Color : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Keywords["black"],
                            Patterns.Keywords["dark_blue"],
                            Patterns.Keywords["dark_green"],
                            Patterns.Keywords["dark_aqua"],
                            Patterns.Keywords["dark_red"],
                            Patterns.Keywords["dark_purple"],
                            Patterns.Keywords["gold"],
                            Patterns.Keywords["gray"],
                            Patterns.Keywords["dark_gray"],
                            Patterns.Keywords["blue"],
                            Patterns.Keywords["green"],
                            Patterns.Keywords["aqua"],
                            Patterns.Keywords["red"],
                            Patterns.Keywords["light_purple"],
                            Patterns.Keywords["yellow"],
                            Patterns.Keywords["white"],
                            Patterns.Keywords["minecoin_gold"],
                            Patterns.Keywords["reset"]
                        });
                }
            }
        }
    }
}
