using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using MCDatapackCompiler.Compiler.Builder;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public class Directive : Unspecific
        {
            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, context) => {
                    // TODO: Register namespace shorteners to lookup
                    context.UsingDirectives.Add(new Builder.Context.Local.UsingDirectives());
                    return "";
                }); 
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
