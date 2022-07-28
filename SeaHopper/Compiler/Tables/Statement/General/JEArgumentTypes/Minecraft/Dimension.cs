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
