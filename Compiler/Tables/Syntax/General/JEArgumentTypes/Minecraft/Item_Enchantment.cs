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
                public class Item_Enchantment : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new(){
                            Patterns.Literals.NameLiteral
                        });
                }
            }
        }
    }
}
