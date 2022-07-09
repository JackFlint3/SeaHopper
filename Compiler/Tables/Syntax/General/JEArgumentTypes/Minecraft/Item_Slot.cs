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
                public class Item_Slot : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            Patterns.Literals.NameLiteral,
                            Patterns.All(new()
                            {
                                Patterns.Literals.NameLiteral,
                                Patterns.Symbols["."],
                                Patterns.Literals.Number
                            }),
                            Patterns.Literals.Number
                        });

                }
            }
        }
    }
}
