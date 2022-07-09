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
                public class Game_Profile : Minecraft
                {
                    // Constraint: Targets need to target a player
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new()
                        {
                            Patterns.Literals.Number,
                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity))
                        });
                }
            }
        }
    }
}
