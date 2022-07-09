using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
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
