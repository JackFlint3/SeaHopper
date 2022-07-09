using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public partial class Command
        {
            public class Say : Command
            {
                public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, prefix) => {
                        string str = "say";
                        if (!string.IsNullOrEmpty(prefix)) str = prefix + " " + str;
                        string literal = expressions[2].Build();
                        str += " " + literal.Substring(1, literal.Length - 2);
                        return str;
                    });
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new() {
                        Patterns.Keywords["say"],
                        Patterns.Symbols["("],
                        Patterns.Literals.StringLiteral,
                        Patterns.Symbols[")"],
                        Patterns.Symbols[";"]
                    });
            }
        }
    }
}
