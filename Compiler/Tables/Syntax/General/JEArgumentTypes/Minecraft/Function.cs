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
                public new class Function : Minecraft
                {
                    // This shouldnt get used, as it should be implemented elsewhere

                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new()
                        {
                            RetrieveByType(typeof(Resource_Location)),
                            Patterns.All(new()
                            {
                                Patterns.Symbols["#"],
                                Patterns.Literals.NameLiteral
                            })
                        });
                }
            }
        }
    }
}
