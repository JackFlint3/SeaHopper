using SeaHopper.Compiler.Pattern;
using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class ScoreHolder : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                            //RetrieveByType(typeof(UUID)),
                            Patterns.Literals.NameLiteral,
                            Patterns.Operators["*"]
                        });
                }
            }
        }
    }
}
