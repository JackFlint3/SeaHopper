using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using System.Text;
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
                    public class If : Subcommand
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(IfEqualsNot)),
                                    RetrieveByType(typeof(IfEquals))
                                })
                            });

                        public class IfEquals : If
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["if"],
                                    RetrieveByType(typeof(SubCommand))
                                });

                        }
                        public class IfEqualsNot : If
                        {
                            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                            {
                                var holder = new StatementHolder(expressions, (expressions, context) => {
                                    string prefix = null;
                                    if (context != null) prefix = context.Data["prefix"];
                                    if (context == null) context = new Builder.Context.BuildContext("");
                                    context.Data["prefix"] = null;

                                    StringBuilder builder = new StringBuilder();
                                    if (!string.IsNullOrEmpty(prefix))
                                    {
                                        builder.Append(prefix + ' ');
                                    }

                                    builder.Append("unless");

                                    int iExpr = 2;
                                    for (; iExpr < expressions.Count - 1; iExpr++)
                                    {
                                        var expr = expressions[iExpr];
                                        builder.Append(' ');
                                        builder.Append(expr.Build(context));
                                    }

                                    context.Data["prefix"] = prefix;
                                    return builder.ToString();
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["if"],
                                    Patterns.Keywords["not"],
                                    RetrieveByType(typeof(SubCommand))
                                });

                        }

                        public class SubCommand : If
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(
                                    GetSubclassesForType(typeof(SubCommand))
                                );

                            public class Block : SubCommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["block"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Pos)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Predicate))
                                    });
                            }
                            public class Blocks : SubCommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["blocks"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Pos)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Pos)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Pos)),
                                        Patterns.Any(new(){
                                            Patterns.One(new()
                                            {
                                                Patterns.Keywords["all"],
                                                Patterns.Keywords["masked"]
                                            })
                                        })
                                    });
                            }
                            public class Data : SubCommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["data"],
                                        Patterns.One(new()
                                        {
                                            RetrieveByType(typeof(EntityData)),
                                            RetrieveByType(typeof(BlockData)),
                                            RetrieveByType(typeof(StorageData))
                                        })
                                    });

                                public class EntityData : Data
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new()
                                        {
                                            Patterns.Keywords["entity"],
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Path))
                                        });

                                }

                                public class BlockData : Data
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new()
                                        {
                                            Patterns.Keywords["block"],
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Pos)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Path))
                                        });

                                }

                                public class StorageData : Data
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new()
                                        {
                                            Patterns.Keywords["storage"],
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Resource_Location)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Path))
                                        });
                                }
                            }
                            public class Entity : SubCommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["entity"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity))
                                    });
                            }
                            public class Predicate : SubCommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["predicate"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Resource_Location))
                                    });
                            }
                            public class Score : SubCommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["score"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.ScoreHolder)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Objective)),
                                        Patterns.One(new()
                                        {
                                            RetrieveByType(typeof(ScoreEqualityComparer)),
                                            RetrieveByType(typeof(ScoreMatches)),
                                        })
                                    });

                                public class ScoreEqualityComparer : Score
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new()
                                        {
                                            Patterns.One(new()
                                            {
                                                RetrieveByType(typeof(LessThan)),
                                                RetrieveByType(typeof(LessOrEqualThan)),
                                                RetrieveByType(typeof(Equal)),
                                                RetrieveByType(typeof(GreaterOrEqualThan)),
                                                RetrieveByType(typeof(GreaterThan))
                                            }),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.ScoreHolder)),
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Objective))
                                        });

                                    public class LessThan : ScoreEqualityComparer
                                    {
                                        public override Pattern<LexerToken> Pattern =>
                                            Patterns.All(new()
                                            {
                                                Patterns.Operators["<"]
                                            });
                                    }
                                    public class LessOrEqualThan : ScoreEqualityComparer
                                    {
                                        public override Pattern<LexerToken> Pattern =>
                                            Patterns.All(new()
                                            {
                                                Patterns.Operators["<"],
                                                Patterns.Operators["="]
                                            });
                                    }
                                    public class Equal : ScoreEqualityComparer
                                    {
                                        public override Pattern<LexerToken> Pattern =>
                                            Patterns.All(new()
                                            {
                                                Patterns.Operators["="]
                                            });
                                    }
                                    public class GreaterOrEqualThan : ScoreEqualityComparer
                                    {
                                        public override Pattern<LexerToken> Pattern =>
                                            Patterns.All(new()
                                            {
                                                Patterns.Operators[">"],
                                                Patterns.Operators["="]
                                            });
                                    }
                                    public class GreaterThan : ScoreEqualityComparer
                                    {
                                        public override Pattern<LexerToken> Pattern =>
                                            Patterns.All(new()
                                            {
                                                Patterns.Operators[">"]
                                            });
                                    }
                                }

                                public class ScoreMatches : Score
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new()
                                        {
                                            Patterns.Keywords["matches"],
                                            RetrieveByType(typeof(JEArgumentTypes.Minecraft.Int_Range))
                                        });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
