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
                public class Team : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            RetrieveByType(typeof(Name)),
                        });

                    public class Name : Minecraft
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Many(
                                    Patterns.One(new()
                                    {
                                        Patterns.Literals.NameLiteral,
                                        Patterns.Literals.Number,
                                        Patterns.Operators["+"],
                                        Patterns.Operators["-"],
                                        Patterns.Symbols["."]
                                    })
                                )
                            });
                    }

                }
            }
        }
    }
}
