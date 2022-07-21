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
                    public class In : Subcommand
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                    Patterns.Keywords["in"],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Dimension))
                            });
                    }
                }
            }
        }
    }
}
