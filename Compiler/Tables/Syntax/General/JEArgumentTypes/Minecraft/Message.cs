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
                public class Message : Minecraft
                {
                    // TODO: Implement actual message parsing, maybe even use string literals instead
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            Patterns.Many(
                                    Patterns.One(new(){
                                        Patterns.Literals.StringLiteral,
                                        RetrieveByType(typeof(Entity))
                                    }),
                                    Patterns.Operators["+"]
                                )
                        });
                }
            }
        }
    }
}
