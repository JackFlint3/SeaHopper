using SeaHopper.Compiler.Pattern;
using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific : Statement
    {
        public abstract partial class JEArgumentTypes : Unspecific
        {
            public abstract partial class Brigadier : JEArgumentTypes
            {
                public class Bool : Brigadier
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Keywords["true"],
                            Patterns.Keywords["false"]
                        });
                }
            }
        }
    }
}
