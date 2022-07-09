using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
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
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    RetrieveByType(typeof(Store.Subcommand))
                                });

                        }
                        public class StoreSuccess : Store
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new() {
                                    Patterns.Keywords["not"],
                                    RetrieveByType(typeof(Store.Subcommand))
                                });
                        }

                        public new class Subcommand : Store
                        {
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
