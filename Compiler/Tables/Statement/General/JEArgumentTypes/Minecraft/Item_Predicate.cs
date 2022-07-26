using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Item_Predicate : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            new Resource_Location(),
                            // TODO: IMPLEMENT DATATAG SUPPORT
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
