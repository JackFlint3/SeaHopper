using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        /// <summary>
        /// A Command to Execute another Command, setting a different environment or changing surrounding criteria
        /// </summary>
        public partial class Command : GeneralContext
        {
            public override Pattern<LexerToken> Pattern =>
                Patterns.All(new() {
                    Patterns.One(new()
                    {
                        RetrieveByType(typeof(Execute)),
                        RetrieveByType(typeof(Say))
                    })
                });
        }
    }
}
