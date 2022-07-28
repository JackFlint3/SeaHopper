using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Pattern;
using SeaHopper.Compiler.Trees.Expressions;
using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public partial class Command
        {
            public partial class Scoreboard : Command
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

                        context.Data["prefix"] = str;
                        str = expressions[0].Build(context);
                        context.Data["prefix"] = prefix;

                        return str;
                    });
                    return holder;
                }

                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new() {
                        RetrieveByType(typeof(Subcommand)),
                        Patterns.Symbols[";"]
                    });

                public partial class Subcommand : Scoreboard
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        return new StatementHolder(expressions);
                    }

                    public override Pattern<LexerToken> Pattern =>
                        new Pattern<LexerToken>.RequiredOne(
                                GetSubclassesForType(typeof(Subcommand))
                            );
                }
            }
        }
    }
}
