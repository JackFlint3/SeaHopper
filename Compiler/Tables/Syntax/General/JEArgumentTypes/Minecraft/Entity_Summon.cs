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
                public class Entity_Summon : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new(){
                            RetrieveByType(typeof(Resource_Location)),
                            Patterns.Literals.NameLiteral
                        });
                }
            }
        }
    }
}
