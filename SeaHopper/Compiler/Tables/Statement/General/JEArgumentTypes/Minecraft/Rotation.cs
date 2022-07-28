using SeaHopper.Compiler.Pattern;
using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Rotation : Minecraft
                {
                    // Constraint: Clamp to degrees
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            Patterns.One(new()
                            {
                                Patterns.Operators["~"],
                                RetrieveByType(typeof(Brigadier.Numerics.Float.Double)),
                                Patterns.All(new()
                                {
                                    Patterns.Operators["~"],
                                    RetrieveByType(typeof(Brigadier.Numerics.Float.Double))
                                })
                            }),
                            Patterns.One(new()
                            {
                                Patterns.Operators["~"],
                                RetrieveByType(typeof(Brigadier.Numerics.Float.Double)),
                                Patterns.All(new()
                                {
                                    Patterns.Operators["~"],
                                    RetrieveByType(typeof(Brigadier.Numerics.Float.Double))
                                })
                            })
                        });
                }
            }
        }
    }
}
