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
