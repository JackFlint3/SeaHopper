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
                public class Message : Minecraft
                {
                    // TODO: Implement actual message parsing, maybe even use string literals instead
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            Patterns.Many(
                                    Patterns.One(new(){
                                        Patterns.Literals.StringLiteral,
                                        RetrieveByType(typeof(Entity))
                                    }),
                                    Patterns.Operators["+"]
                                )
                        });
                }
            }
        }
    }
}
