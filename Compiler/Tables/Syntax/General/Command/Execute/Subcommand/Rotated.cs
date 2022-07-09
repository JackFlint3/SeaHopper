using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public partial class Command
        {
            public partial class Execute
            {
                public partial class Subcommand
                {
                    public class Rotated : Subcommand
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Keywords["positioned"],
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(RotatedPos)),
                                    RetrieveByType(typeof(RotatedAs))
                                })
                            });
                        public class RotatedPos : Rotated
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Rotation))
                                });
                        }
                        public class RotatedAs : Rotated
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["as"],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                });
                        }
                    }
                }
            }
        }
    }
}
