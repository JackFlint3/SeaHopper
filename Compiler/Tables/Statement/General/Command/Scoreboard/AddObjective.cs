using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public partial class Command
        {
            public partial class Scoreboard
            {

                public partial class Subcommand
                {
                    public class AddObjective : Subcommand
                    {
                        public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                        {
                            var holder = new StatementHolder(expressions, (expressions, context) => {
                                string str;
                                string prefix = null;
                                if (context != null) prefix = context.Data["prefix"];
                                if (context == null) context = new Builder.Context.BuildContext("");

                                if (string.IsNullOrEmpty(prefix))
                                    str = "";
                                else str = prefix + " ";

                                str += "scoreboard objectives add ";

                                context.Data["prefix"] = null;
                                str += expressions[2].Build(context);
                                context.Data["prefix"] = prefix;

                                str += " dummy";

                                return str;
                            });
                            return holder;
                        }

                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Keywords["score"],
                                Patterns.Keywords["add"],
                                Patterns.Literals.NameLiteral
                            });
                    }
                }
            }
        }
    }
}
