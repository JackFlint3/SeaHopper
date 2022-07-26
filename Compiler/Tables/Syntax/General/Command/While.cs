using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using System;
using MCDatapackCompiler.Compiler.Lexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public partial class Command
        {
            public class While : Command
            {
                public override bool Match(IExpressionBuilder<StreamLexer.LexerToken> builder)
                {
                    builder.Prepare(this);

                    if (Pattern.Match(builder))
                    {

                        var expr = builder.Collapse();
                        if (expr != null) builder.Collect(expr);

                        return true;
                    }
                    else
                    {
                        builder.Discard();
                        return false;
                    }
                }

                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                {
                    var holder = new StatementHolder(expressions, (expressions, context) => {
                        string str;
                        string prefix = "";
                        if (context != null) prefix = context.Data["prefix"];
                        if (context == null) context = new Builder.Context.BuildContext("");

                        if (!string.IsNullOrEmpty(prefix)) str = prefix + " ";
                        else str = "";
                        context.Data["prefix"] = null;


                        string anonymousName = "__anonymousfunction_while_" + this.GetHashCode().ToString();
                        string predicate = "execute if " + expressions[2].Build(context) + " run function " + context.CurrentNamespace.Identifier + ":" + anonymousName;

                        // Building inner anonymous function which will loop until the predicate is false or the command limit is exceeded
                        Builder.Context.Global.Function function = new Builder.Context.Global.Function(anonymousName, (StatementHolder)expressions[4]);
                        var currentFun = context.CurrentFunction;
                        if (context.CurrentNamespace != null)
                            context.CurrentNamespace.AddFunction(function);

                        function.Build(context);
                        // TODO: Build Expression instead of appending a literal string
                        function.AppendBody(predicate);

                        context.CurrentFunction = currentFun;
                        context.Data["prefix"] = prefix;

                        return str + predicate;
                    });
                    return holder;
                }
                public override Pattern<LexerToken> Pattern =>
                    Patterns.All(new() {
                        Patterns.Keywords["while"],
                        Patterns.Symbols["("],
                        Patterns.One(new()
                        {
                            RetrieveByType(typeof(Execute.Subcommand.If.SubCommand)),
                            RetrieveByType(typeof(NativePredicate))
                        }),
                        Patterns.Symbols[")"],
                        Patterns.One(new()
                        {
                            Patterns.All(new()
                            {
                                RetrieveByType(typeof(Command)),
                                Patterns.Symbols[";"]
                            }),
                            RetrieveByType(typeof(Body))
                        })
                    });

                public class NativePredicate : While
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        var holder = new StatementHolder(expressions, (expressions, context) => {
                            string str;
                            string prefix = "";
                            if (context != null) prefix = context.Data["prefix"];
                            if (context == null) context = new Builder.Context.BuildContext("");

                            if (!string.IsNullOrEmpty(prefix)) str = prefix + " ";
                            else str = "";
                            context.Data["prefix"] = null;
                            string target = context.Data["target"];
                            string path = context.Data["path"];

                            // Expression writes data into context, which then can be used to build command
                            expressions[0].Build(context);
                            str += "score " + context.Data["target"] + " " + context.Data["path"] + " ";
                            str += expressions[1].Build(context);
                            if (expressions.Count == 4) str += expressions[2].Build(context);

                            expressions[expressions.Count - 1].Build(context);
                            str += " " + context.Data["target"] + " " + context.Data["path"];

                            context.Data["prefix"] = prefix;
                            context.Data["target"] = target;
                            context.Data["path"] = path;

                            return str;
                        });
                        return holder;
                    }
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            RetrieveByType(typeof(VarReference)),
                            Patterns.One(new()
                            {
                                Patterns.Operators["="],
                                Patterns.All(new()
                                {
                                    Patterns.Operators["<"],
                                    Patterns.Operators["="],
                                }),
                                Patterns.Operators["<"],
                                Patterns.All(new()
                                {
                                    Patterns.Operators[">"],
                                    Patterns.Operators["="],
                                }),
                                Patterns.Operators[">"],
                            }),
                            RetrieveByType(typeof(VarReference))
                        });
                }

                public class VarReference : While
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        return new StatementHolder(expressions);
                    }

                    public override Pattern<LexerToken> Pattern =>
                    Patterns.One(new() {
                        RetrieveByType(typeof(ExplicitSelf)),
                        RetrieveByType(typeof(ImplicitSelf))
                    });

                    public class ImplicitSelf : VarReference
                    {
                        public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                        {
                            var holder = new StatementHolder(expressions, (expressions, context) => {
                                string str;
                                string prefix = "";
                                if (context != null) prefix = context.Data["prefix"];
                                if (context == null) context = new Builder.Context.BuildContext("");

                                if (!string.IsNullOrEmpty(prefix)) str = prefix;
                                else str = "";
                                context.Data["prefix"] = null;

                                context.Data["path"] = expressions[0].Build(context);
                                context.Data["target"] = "@s";
                                
                                context.Data["prefix"] = prefix;

                                return str;
                            });
                            return holder;
                        }

                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Literals.NameLiteral
                            });
                    }

                    public class ExplicitSelf : VarReference
                    {
                        public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                        {
                            var holder = new StatementHolder(expressions, (expressions, context) => {
                                string str;
                                string prefix = "";
                                if (context != null) prefix = context.Data["prefix"];
                                if (context == null) context = new Builder.Context.BuildContext("");

                                if (!string.IsNullOrEmpty(prefix)) str = prefix;
                                else str = "";
                                context.Data["prefix"] = null;

                                context.Data["path"] = expressions[0].Build(context);
                                context.Data["target"] = expressions[1].Build(context);

                                context.Data["prefix"] = prefix;

                                return str;
                            });
                            return holder;
                        }

                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Literals.NameLiteral,
                                RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity))
                            });
                    }
                }

            }
        }
    }
}
