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
                public new class Function : Minecraft
                {
                    // This shouldnt get used, as it should be implemented elsewhere

                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new()
                        {
                            RetrieveByType(typeof(Resource_Location)),
                            Patterns.All(new()
                            {
                                Patterns.Symbols["#"],
                                Patterns.Literals.NameLiteral
                            })
                        });
                }
            }
        }
    }
}
