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
            public partial class Execute
            {
                public partial class Subcommand
                {
                    public class Positioned : Subcommand
                    {
                        public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                        {
                            var holder = new StatementHolder(expressions, (expressions, context) => {
                                string prefix = null;
                                if (context != null) prefix = context.Data["prefix"];
                                if (context == null) context = new Builder.Context.BuildContext("");
                                context.Data["prefix"] = null;

                                string str = "";

                                if (!string.IsNullOrEmpty(prefix))
                                {
                                    str = prefix + " " + str;
                                }

                                str += expressions[0].Build(context) + ' ';
                                str += expressions[1].Build(context);

                                context.Data["prefix"] = prefix;
                                return str;
                            });
                            return holder;
                        }

                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Keywords["positioned"],
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(PositionedPos)),
                                    RetrieveByType(typeof(PositionedAs))
                                })
                            });

                        public class PositionedPos : Positioned
                        {
                            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                            {
                                var holder = new StatementHolder(expressions, (expressions, context) => {
                                    return expressions[1].Build(context);
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Symbols["("],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Vector3)),
                                    Patterns.Symbols[")"]
                                });
                        }
                        public class PositionedAs : Positioned
                        {
                            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                            {
                                var holder = new StatementHolder(expressions, (expressions, context) => {
                                    return expressions[0].Build(context) + ' ' +
                                        expressions[2].Build(context);
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["as"],
                                    Patterns.Symbols["("],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                    Patterns.Symbols[")"]
                                });
                        }
                    }
                }
            }
        }
    }
}
