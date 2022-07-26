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
                public class Block_Predicate : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            RetrieveByType(typeof(Resource_Location)),
                            Patterns.Any(new()
                            {
                                Patterns.Symbols["["],
                                Patterns.Many(RetrieveByType(typeof(Block_State)), Patterns.Symbols[","]),
                                Patterns.Symbols["]"]
                            })
                            // TODO: Implement Datatag support
                            //Patterns.Any(new()
                            //{
                            //    Patterns.Symbols["{"],
                            //    Patterns.AnyMany(new SNBT(), Patterns.Symbols[","]),
                            //    Patterns.Symbols["}"]
                            //})
                        });

                }
            }
        }
    }
}
