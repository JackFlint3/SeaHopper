using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Entity : Minecraft
                {
                    public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
                    {
                        var holder = new StatementHolder(expressions, (expressions, context) => {
                            string str;
                            string prefix = null;
                            if (context != null) { 
                                prefix = context.Data["prefix"];
                                context.Data["prefix"] = null;
                            }

                            if (string.IsNullOrEmpty(prefix))
                                str = "";
                            else str = prefix;

                            if (expressions.Count == 0) return str;

                            int iExpr = 0;
                            for (; iExpr < expressions.Count - 1; iExpr++)
                            {
                                var expr = expressions[iExpr];
                                str += "" + expr.Build(context);
                            }

                            str = str + expressions[iExpr].Build(context);

                            return str;
                        });
                        return holder;
                    }

                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new(){
                            Patterns.Literals.NameLiteral,
                            //RetrieveByType(typeof(UUID)),
                            Patterns.All(new()
                            {
                                Patterns.Symbols["@"],
                                Patterns.One(new()
                                {
                                   Patterns.Keywords["p"],
                                   Patterns.Keywords["r"],
                                   Patterns.Keywords["a"],
                                   Patterns.Keywords["e"],
                                   Patterns.Keywords["s"]
                                }),
                                RetrieveByType(typeof(Selector))
                            })
                        });

                    public class PlayerEntity : Minecraft.Entity
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Keywords["p"]
                            });
                    }
                    public class RandomEntity : Minecraft.Entity
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Keywords["r"]
                            });
                    }
                    public class AllEntity : Minecraft.Entity
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Keywords["a"]
                            });
                    }
                    public class EntityEntity : Minecraft.Entity
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Keywords["e"]
                            });
                    }
                    public class SelfEntity : Minecraft.Entity
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Keywords["s"]
                            });
                    }

                    public class Selector : Minecraft.Entity
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.Any(new()
                                {
                                    Patterns.All(new()
                                    {
                                        Patterns.Symbols["["],
                                        Patterns.Any(new()
                                        {
                                            Patterns.Many(
                                                    new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType())),
                                                Patterns.Symbols[","])
                                        }),
                                        Patterns.Symbols["]"]
                                    })
                                });


                        public class SingleAxes : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                RetrieveByType(typeof(JEArgumentTypes.Parameters.ReducedSwizzle)),
                                Patterns.Operators["="],
                                RetrieveByType(typeof(Brigadier.Numerics.Float))
                                });
                        }
                        public class Distance : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                Patterns.Keywords["distance"],
                                Patterns.Operators["="],
                                RetrieveByType(typeof(Float_Range))
                                });
                        }
                        public class Volume : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                RetrieveByType(typeof(JEArgumentTypes.Parameters.Volume)),
                                Patterns.Operators["="],
                                RetrieveByType(typeof(Brigadier.Numerics.Float))
                                });
                        }
                        public class Scores : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                Patterns.Keywords["scores"],
                                Patterns.Operators["="],
                                Patterns.Symbols["{"],
                                Patterns.Many(RetrieveByType(typeof(ScorePredicate)),Patterns.Symbols[","]),
                                Patterns.Symbols["}"]
                                });

                            public class ScorePredicate : JEArgumentTypes
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.One(new()
                                    {
                                        Patterns.All(new()
                                        {
                                            RetrieveByType(typeof(JEArgumentTypes.Parameters.NameLiteral)),
                                            Patterns.Operators["="],
                                            RetrieveByType(typeof(Minecraft.Float_Range))
                                        })
                                    });
                            }
                        }
                        public new class Tag : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class TagEquals : Tag
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["tag"],
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(JEArgumentTypes.Parameters.NameLiteral))
                                    });
                            }
                            public class TagEqualsNot : Tag
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["tag"],
                                        Patterns.Operators["="],
                                        Patterns.Operators["!"],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.NameLiteral))
                                    });
                            }
                        }
                        public new class Team : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class TeamEquals : Team
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["team"],
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(Minecraft.Team))
                                    });
                            }
                            public class TeamEqualsNot : Team
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["team"],
                                    Patterns.Operators["="],
                                    Patterns.Operators["!"],
                                    RetrieveByType(typeof(Minecraft.Team))
                                    });
                            }
                        }
                        public class Limit : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                Patterns.Keywords["limit"],
                                Patterns.Operators["="],
                                RetrieveByType(typeof(Brigadier.Numerics.Int)),
                                Patterns.Any(new()
                                {
                                    Patterns.All(new()
                                    {
                                        Patterns.Symbols[","],
                                        RetrieveByType(typeof(Sort))
                                    })
                                })
                                });

                            public class Sort : Limit
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["sort"],
                                        Patterns.Operators["="],
                                        Patterns.One(new()
                                        {
                                            Patterns.Keywords["nearest"],
                                            Patterns.Keywords["furthest"],
                                            Patterns.Keywords["random"],
                                            Patterns.Keywords["arbitrary"]
                                        })
                                    });
                            }
                        }
                        public class Level : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                    Patterns.Keywords["level"],
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(Float_Range))
                                });
                        }
                        public class Gamemode : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class GamemodeEquals : Gamemode
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["gamemode"],
                                        Patterns.Operators["="],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.Gamemode))
                                    });
                            }

                            public class GamemodeEqualsNot : Gamemode
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["gamemode"],
                                        Patterns.Operators["="],
                                        Patterns.Operators["!"],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.Gamemode))
                                    });
                            }
                        }
                        public class Name : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class NameEquals : Name
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["name"],
                                        Patterns.Operators["="],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.GivenName))
                                    });
                            }

                            public class NameEqualsNot : Name
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["name"],
                                        Patterns.Operators["="],
                                        Patterns.Operators["!"],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.GivenName))
                                    });
                            }
                        }
                        public new class Rotation : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class X_Rotation : Rotation
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["x_rotation"],
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(Float_Range))
                                    });
                            }
                            public class Y_Rotation : Rotation
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["y_rotation"],
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(Float_Range))
                                    });
                            }
                        }
                        public class Type : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class TypeEquals : Type
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["type"],
                                        Patterns.Operators["="],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.EntityType))
                                    });
                            }
                            public class TypeEqualsNot : Type
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                        Patterns.Keywords["type"],
                                        Patterns.Operators["="],
                                        Patterns.Operators["!"],
                                        RetrieveByType(typeof(JEArgumentTypes.Parameters.EntityType))
                                    });
                            }
                        }
                        public new class NBT : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                            public class NBTEquals : NBT
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["nbt"],
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Compound_Tag))
                                    });
                            }
                            public class NBTEqualsNot : NBT
                            {
                                public override Pattern<LexerToken> Pattern =>
                                    Patterns.All(new()
                                    {
                                    Patterns.Keywords["nbt"],
                                    Patterns.Operators["="],
                                    Patterns.Operators["!"],
                                    RetrieveByType(typeof(JEArgumentTypes.Minecraft.NBT_Compound_Tag))
                                    });
                            }
                        }
                        public class Advancements : Selector
                        {
                            public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                    Patterns.Keywords["advancements"],
                                    Patterns.Operators["="],
                                    Patterns.Symbols["{"],
                                    RetrieveByType(typeof(Resource_Location)),
                                    Patterns.Operators["="],
                                    RetrieveByType(typeof(Brigadier.Bool)),
                                    Patterns.Symbols["}"]
                                });
                        }
                    }
                }
            }
        }
    }
}
