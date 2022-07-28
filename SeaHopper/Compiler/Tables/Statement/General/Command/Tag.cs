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
            public class Tag : Command
            {
                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, context) => {
                        return expressions[0].Build(context);
                    });
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.One(new() {
                        RetrieveByType(typeof(AddTag)),
                        RetrieveByType(typeof(RemoveTag))
                    });

                public class AddTag : Tag
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        var holder = new StatementHolder(expressions, (expressions, context) => {
                            string str = "tag";
                            string prefix = null;
                            if (context != null) prefix = context.Data["prefix"];
                            if (context == null) context = new Builder.Context.BuildContext("");

                            if (!string.IsNullOrEmpty(prefix)) str = prefix + " " + str;
                            context.Data["prefix"] = null;
                            string target = expressions[1].Build(context);
                            string literal = expressions[4].Build(context);
                            context.Data["prefix"] = prefix;

                            str += " " + target + " add " + literal;
                            
                            return str;
                        });
                        return holder;
                    }

                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            Patterns.Keywords["tag"],
                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                            Patterns.Operators["+"],
                            Patterns.Operators["="],
                            Patterns.Literals.NameLiteral,
                            Patterns.Symbols[";"]
                        });
                }

                public class RemoveTag : Tag
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        var holder = new StatementHolder(expressions, (expressions, context) => {
                            string str = "tag";
                            string prefix = null;
                            if (context != null) prefix = context.Data["prefix"];
                            if (context == null) context = new Builder.Context.BuildContext("");

                            if (!string.IsNullOrEmpty(prefix)) str = prefix + " " + str;
                            context.Data["prefix"] = null;
                            string target = expressions[1].Build(context);
                            string literal = expressions[4].Build(context);
                            context.Data["prefix"] = prefix;

                            str += " " + target + " remove " + literal;

                            return str;
                        });
                        return holder;
                    }

                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            Patterns.Keywords["tag"],
                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                            Patterns.Operators["-"],
                            Patterns.Operators["="],
                            Patterns.Literals.NameLiteral,
                            Patterns.Symbols[";"]
                        });
                }
            }
        }
    }
}
