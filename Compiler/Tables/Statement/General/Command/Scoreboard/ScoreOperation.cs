using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

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
                    public class ScoreOperation : Subcommand
                    {
                        public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                        {
                            return new StatementHolder(expressions);
                        }

                        public override Pattern<LexerToken> Pattern =>
                            Patterns.One(new() {
                                RetrieveByType(typeof(AssignTarget)),
                                RetrieveByType(typeof(AssignSelf))
                            });

                        public class AssignSelf : ScoreOperation
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

                                    str += "scoreboard players ";

                                    context.Data["prefix"] = null;
                                    context.Data["path"] = expressions[0].Build(context);
                                    context.Data["target"] = "@s";
                                    str += expressions[1].Build(context);
                                    context.Data["prefix"] = prefix;
                                    context.Data["path"] = null;
                                    context.Data["target"] = null;

                                    return str;
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Literals.NameLiteral,
                                    Patterns.One(new()
                                    {
                                        new Pattern<LexerToken>.RequiredOne(
                                            GetSubclassesForType(typeof(AssignOperations)))
                                    })
                                });
                        }

                        public class AssignTarget : ScoreOperation
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

                                    str += "scoreboard players ";

                                    context.Data["prefix"] = null;
                                    context.Data["path"] = expressions[0].Build(context);
                                    context.Data["target"] = expressions[1].Build(context);
                                    str += expressions[2].Build(context);
                                    context.Data["prefix"] = prefix;
                                    context.Data["path"] = null;
                                    context.Data["target"] = null;

                                    return str;
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Literals.NameLiteral,
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                    Patterns.One(new()
                                    {
                                        new Pattern<LexerToken>.RequiredOne(
                                            GetSubclassesForType(typeof(AssignOperations)))
                                    })
                                });
                        }

                        public abstract class AssignOperations : ScoreOperation
                        {
                            public class AssignScore : AssignOperations
                            {
                                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                                {
                                    var holder = new StatementHolder(expressions, (expressions, context) =>
                                    {
                                        string str;
                                        string target = context.Data["target"];
                                        string path = context.Data["path"];
                                        str = "set " + target + " " + path + " ";
                                        str += expressions[1].Build(context);

                                        return str;
                                    });
                                    return holder;
                                }

                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Operators["="],
                                        Patterns.Literals.Number
                                    });
                            }
                            public class AddScore : AssignOperations
                            {
                                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                                {
                                    var holder = new StatementHolder(expressions, (expressions, context) =>
                                    {
                                        string str;
                                        string target = context.Data["target"];
                                        string path = context.Data["path"];
                                        str = "add " + target + " " + path + " ";
                                        str += expressions[2].Build(context);

                                        return str;
                                    });
                                    return holder;
                                }

                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Operators["+"],
                                        Patterns.Operators["="],
                                        Patterns.Literals.Number
                                    });
                            }

                            public class RemoveScore : AssignOperations
                            {
                                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                                {
                                    var holder = new StatementHolder(expressions, (expressions, context) =>
                                    {
                                        string str;
                                        string target = context.Data["target"];
                                        string path = context.Data["path"];
                                        str = "remove " + target + " " + path + " ";
                                        str += expressions[2].Build(context);

                                        return str;
                                    });
                                    return holder;
                                }

                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Operators["-"],
                                        Patterns.Operators["="],
                                        Patterns.Literals.Number
                                    });
                            }

                            public class OperationScore : AssignOperations
                            {
                                public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                                {
                                    var holder = new StatementHolder(expressions, (expressions, context) =>
                                    {
                                        string str;
                                        string target = context.Data["target"];
                                        string path = context.Data["path"];
                                        str = "operation " + target + " " + path + " ";
                                        str += expressions[0].Build(context) + " ";

                                        if (expressions.Count == 3)
                                            str += expressions[2].Build(context) + " ";
                                        else
                                            str += "@s ";

                                        str += expressions[1].Build(context);

                                        return str;
                                    });
                                    return holder;
                                }

                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.One(new(){
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Operation.Multiplication)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Operation.FloorDivision)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Operation.Modulus)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Operation.Swapping)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Operation.ChoosingMaximum)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Operation.ChoosingMinimum))
                                        }),
                                        Patterns.One(new()
                                        {
                                            Patterns.All(new()
                                            {
                                                Patterns.Literals.NameLiteral,
                                                RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                            }),
                                            Patterns.Literals.NameLiteral
                                        })
                                    });
                            }
                        }

                    }
                }
            }
        }
    }
}
