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
                public class Component : Minecraft
                {
                    //Raw JSON text, custom match might be required here

                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            Patterns.Literals.StringLiteral,
                            Patterns.Literals.NameLiteral,
                            RetrieveByType(typeof(NBT.SNBT))
                        });
                }
            }
        }
    }
}
