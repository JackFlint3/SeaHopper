using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific : Statement
    {
        public abstract partial class JEArgumentTypes : Unspecific
        {
            public abstract partial class Brigadier : JEArgumentTypes
            {
                public class Bool : Brigadier
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Keywords["true"],
                            Patterns.Keywords["false"]
                        });
                }
            }
        }
    }
}
