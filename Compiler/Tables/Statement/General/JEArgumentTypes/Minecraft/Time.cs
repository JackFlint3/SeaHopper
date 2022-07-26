using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Time : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            RetrieveByType(typeof(Brigadier.Numerics.Float)),
                            Patterns.Any(new()
                            {
                                Patterns.One(new()
                                {
                                    Patterns.Keywords["d"],
                                    Patterns.Keywords["s"],
                                    Patterns.Keywords["t"]
                                })
                            })
                        });
                }
            }
        }
    }
}
