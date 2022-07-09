using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
public abstract partial class JEArgumentTypes
        {
            public abstract partial class Parameters
            {
                public class DataType : JEArgumentTypes
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Keywords["byte"],
                            Patterns.Keywords["short"],
                            Patterns.Keywords["int"],
                            Patterns.Keywords["long"],
                            Patterns.Keywords["float"],
                            Patterns.Keywords["double"]
                        });
                }
            }
        }
    }
}
