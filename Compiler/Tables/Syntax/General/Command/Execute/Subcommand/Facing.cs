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
                    public class Facing : Subcommand
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Keywords["facing"],
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(FacingPos)),
                                    RetrieveByType(typeof(FacingEntity))
                                })
                            });
                        public class FacingPos : Facing
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Vector3))
                                });

                        }

                        public class FacingEntity : Facing
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["entity"],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Anchor))
                                });
                        }
                    }
                }
            }
        }
    }
}
