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
            public class Function : Command
            {
                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, context) => {
                        string str = "function";
                        string prefix = null;
                        if (context != null) prefix = context.Data["prefix"];
                        if (context == null) context = new Builder.Context.BuildContext("");

                        if (!string.IsNullOrEmpty(prefix)) str = prefix + " " + str;
                        context.Data["prefix"] = null;

                        str += ' ' + expressions[0].Build(context);

                        context.Data["prefix"] = prefix;

                        return str;
                    });
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new() {
                        Patterns.One(new()
                        {
                            RetrieveByType(typeof(ImplicitResource)),
                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Resource))
                        }),
                        Patterns.Symbols["("],
                        Patterns.Symbols[")"],
                        Patterns.Symbols[";"]
                    });

                public class ImplicitResource : Function
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        var holder = new StatementHolder(expressions, (expressions, context) => {
                            return context.CurrentNamespace.Identifier + ":" + expressions[0].Build(context);
                        });
                        return holder;
                    }
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            Patterns.Literals.NameLiteral
                        });
                }
            }
        }
    }
}
