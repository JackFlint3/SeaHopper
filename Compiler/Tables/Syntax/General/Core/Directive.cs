using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public class Directive : GeneralContext
        {
            public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, prefix) => ""); // TODO: Register namespace shorteners to lookup
                return holder;
            }
            public override Pattern<LexerToken> Pattern =>
                Patterns.All(new()
                {
                    Patterns.Keywords["using"],
                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Resource_Location.Namespace)),
                    Patterns.Symbols[";"]
                });
        }
    }
}
