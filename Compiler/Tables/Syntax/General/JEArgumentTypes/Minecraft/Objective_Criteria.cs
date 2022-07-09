using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Objective_Criteria : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new(){
                            Patterns.Many(Patterns.One(new()
                            {
                                Patterns.Literals.NameLiteral
                            }), Patterns.Symbols["."]),
                            Patterns.Any(new()
                            {
                                Patterns.Operators[":"],
                                Patterns.Many(Patterns.One(new()
                                {
                                    Patterns.Literals.NameLiteral
                                }), Patterns.Symbols["."])
                            })
                        });
                }
            }
        }
    }
}
