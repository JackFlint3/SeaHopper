using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public partial class Command
        {
            public partial class Execute
            {
                public partial class Subcommand
                {
                    public class Positioned : Subcommand
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Keywords["positioned"],
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(PositionedPos)),
                                    RetrieveByType(typeof(PositionedAs))
                                })
                            });
                        public class PositionedPos : Positioned
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Vector3))
                                });
                        }
                        public class PositionedAs : Positioned
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
