using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        /// <summary>
        /// A Command to Execute another Command, setting a different environment or changing surrounding criteria
        /// </summary>
        public partial class Command : Unspecific
        {
            public override Pattern<LexerToken> Pattern =>
                Patterns.All(new() {
                    Patterns.One(new()
                    {
                        RetrieveByType(typeof(Execute)),
                        // say(<literal>)
                        RetrieveByType(typeof(Say)),
                        // kill(<target>)
                        RetrieveByType(typeof(Kill)),
                        // tag <target> ['+='|'-='] <literal>
                        RetrieveByType(typeof(Tag)),
                        RetrieveByType(typeof(While)),
                        RetrieveByType(typeof(Function)),
                        // [score [add|remove] <path>|<path> [<target>] [=|-=|+=] <number>|<path> [<target>] [%=|*=|/=|<|>|><] <path> [<target>]]
                        RetrieveByType(typeof(Scoreboard))
                    })
                });
        }
    }
}
