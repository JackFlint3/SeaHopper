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
                public class Column_Pos : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new() {
                            Patterns.One(new() {
                                Patterns.Any(new(){
                                    Patterns.Operators["~"],
                                    RetrieveByType(typeof(Brigadier.Numerics.Int))
                                }),
                                Patterns.Operators["~"],
                                RetrieveByType(typeof(Brigadier.Numerics.Int))
                            }),
                            Patterns.One(new() {
                                Patterns.Any(new(){
                                    Patterns.Operators["~"],
                                    RetrieveByType(typeof(Brigadier.Numerics.Int))
                                }),
                                Patterns.Operators["~"],
                                RetrieveByType(typeof(Brigadier.Numerics.Int))
                            })
                        });
                }
            }
        }
    }
}
