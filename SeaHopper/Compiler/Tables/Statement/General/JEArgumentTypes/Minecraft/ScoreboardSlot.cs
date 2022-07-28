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
                public class ScoreboardSlot : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Keywords["list"],
                            Patterns.All(new()
                            {
                                Patterns.Keywords["sidebar"],
                                Patterns.Any(new(){
                                    Patterns.All(new()
                                    {
                                        Patterns.Symbols["."],
                                        Patterns.Keywords["team"],
                                        Patterns.Symbols["."],
                                        RetrieveByType(typeof(Color))
                                    })
                                })
                            }),
                            Patterns.Keywords["belowName"]
                        });

                }
            }
        }
    }
}
