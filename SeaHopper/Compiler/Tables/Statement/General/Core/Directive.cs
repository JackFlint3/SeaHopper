using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;
using SeaHopper.Compiler.Pattern;
using SeaHopper.Compiler.Trees.Expressions;
using SeaHopper.Compiler.Builder;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
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
