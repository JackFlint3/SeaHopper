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
                public class Swizzle : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Keywords["x"],
                            Patterns.Keywords["y"],
                            Patterns.Keywords["z"],
                            Patterns.Keywords["xy"],
                            Patterns.Keywords["yx"],
                            Patterns.Keywords["xz"],
                            Patterns.Keywords["zx"],
                            Patterns.Keywords["yz"],
                            Patterns.Keywords["zy"],
                            Patterns.Keywords["xyz"],
                            Patterns.Keywords["xzy"],
                            Patterns.Keywords["yzx"],
                            Patterns.Keywords["yxz"],
                            Patterns.Keywords["zxy"],
                            Patterns.Keywords["zyx"]
                        });
                }
            }
        }
    }
}
