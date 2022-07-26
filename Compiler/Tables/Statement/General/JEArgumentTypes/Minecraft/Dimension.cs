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
                public class Dimension : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        // TODO: Add Constraint to only allow specified dimensions
                        Patterns.One(new() {
                            RetrieveByType(typeof(Resource_Location))
                        });
                }
            }
        }
    }
}
