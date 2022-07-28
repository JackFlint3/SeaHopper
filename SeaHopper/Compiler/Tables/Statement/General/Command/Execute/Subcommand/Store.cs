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
            public partial class Execute
            {
                public partial class Subcommand
                {
                    public class Store : Subcommand
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Keywords["store"],
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(StoreResult)),
                                    RetrieveByType(typeof(StoreSuccess))
                                })
                            });

                        public class StoreResult : Store
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
                                    str += expressions[2].Build(context);

                                    context.Data["prefix"] = prefix;
                                    return str;
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["result"],
                                    Patterns.Symbols["("],
                                    RetrieveByType(typeof(Store.Subcommand)),
                                    Patterns.Symbols[")"]
                                });

                        }
                        public class StoreSuccess : Store
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
                                    str += expressions[2].Build(context);

                                    context.Data["prefix"] = prefix;
                                    return str;
                                });
                                return holder;
                            }

                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["success"],
                                    Patterns.Symbols["("],
                                    RetrieveByType(typeof(Store.Subcommand)),
                                    Patterns.Symbols[")"]
                                });
                        }

                        public new class Subcommand : Store
                        {
                            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                            {
                                return new StatementHolder(expressions);
                            }
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.One(new() {
                                    RetrieveByType(typeof(BlockStore)),
                                    RetrieveByType(typeof(BossbarStore)),
                                    RetrieveByType(typeof(EntityStore)),
                                    RetrieveByType(typeof(ScoreStore)),
                                    RetrieveByType(typeof(StorageStore))
                                });


                            public class BlockStore : Subcommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Keywords["block"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Block_Pos)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Path)),
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.DataType)),
                                        RetrieveByType(typeof(JEArgumentTypes.Brigadier.Numerics.Float.Double))
                                    });
                            }
                            public class BossbarStore : Subcommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Keywords["bossbar"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Resource_Location)),
                                        Patterns.One(new()
                                        {
                                            RetrieveByType(typeof(BBValue)),
                                            RetrieveByType(typeof(BBMax))
                                        })
                                    });
                                public class BBValue : BossbarStore
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new() {
                                            Patterns.Keywords["value"]
                                        });
                                }
                                public class BBMax : BossbarStore
                                {
                                    public override Pattern<LexerToken> Pattern =>
                                        Patterns.All(new() {
                                            Patterns.Keywords["max"]
                                        });
                                }
                            }
                            public class EntityStore : Subcommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Keywords["entity"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Entity)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Path)),
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.DataType)),
                                        RetrieveByType(typeof(JEArgumentTypes.Brigadier.Numerics.Float.Double))
                                    });
                            }
                            public class ScoreStore : Subcommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Keywords["score"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.ScoreHolder)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Objective))
                                    });
                            }
                            public class StorageStore : Subcommand
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new() {
                                        Patterns.Keywords["storage"],
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.Resource_Location)),
                                        RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Path)),
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.DataType)),
                                        RetrieveByType(typeof(JEArgumentTypes.Brigadier.Numerics.Float.Double))
                                    });
                            }
                        }
                    }
                }
            }
        }
    }
}
