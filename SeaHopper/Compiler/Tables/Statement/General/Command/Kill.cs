using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Pattern;
using SeaHopper.Compiler.Trees.Expressions;
using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;
using System;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public partial class Command
        {
            public class Kill : Command
            {
                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, context) => {
                        string str = "kill";
                        string prefix = null;
                        if (context != null) prefix = context.Data["prefix"];
                        if (context == null) context = new Builder.Context.BuildContext("");

                        if (!string.IsNullOrEmpty(prefix)) str = prefix + " " + str;
                        context.Data["prefix"] = null;
                        string literal = expressions[2].Build(context);
                        context.Data["prefix"] = prefix;

                        str += string.Concat(" ", literal.AsSpan(1, literal.Length - 2));
                        return str;
                    });
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new() {
                        Patterns.Keywords["kill"],
                        Patterns.Symbols["("],
                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                        Patterns.Symbols[")"],
                        Patterns.Symbols[";"]
                    });
            }
        }
    }
}
