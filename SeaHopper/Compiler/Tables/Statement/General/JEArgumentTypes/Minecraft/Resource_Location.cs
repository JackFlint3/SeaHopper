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
                public class Resource_Location : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.All(new(){
                            Patterns.Any(new(){ RetrieveByType(typeof(Namespace)) }),
                            Patterns.Operators[":"],
                            RetrieveByType(typeof(Path))
                        });

                    public new class Namespace : Resource_Location
                    {
                        // Constraint: Has to exist
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Many(Patterns.One(new()
                                {
                                    Patterns.Literals.NameLiteral
                                }), Patterns.Symbols["."]),
                            });

                    }

                    public class Path : Resource_Location
                    {
                        // Constraint: Has to exist
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.One(new(){
                                Patterns.Many(Patterns.One(new()
                                {
                                    Patterns.Literals.NameLiteral
                                }), Patterns.Symbols["."]),
                                Patterns.Many(Patterns.One(new()
                                {
                                    Patterns.Literals.NameLiteral
                                }), Patterns.Operators["/"]),
                            });
                    }
                }
            }
        }
    }
}
