using SeaHopper.Compiler.Pattern;
using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific : Statement
    {
        public abstract partial class JEArgumentTypes : Unspecific
        {
            public abstract partial class Brigadier : JEArgumentTypes
            {
                public abstract partial class Numerics : Brigadier
                {
                    public class Int : Numerics
                    {
                        // TODO: Contraint from (−2,147,483,648) to (2,147,483,647).
                        public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                    Patterns.Any(new()
                                    {
                                        Patterns.Operators["-"]
                                    }),
                                    Patterns.Literals.Number
                                });
                        public class Long : Int
                        {
                            //TODO: Contraint from (−9,223,372,036,854,775,808) to (9,223,372,036,854,775,807).
                        }
                    }
                }
            }
        }
    }
}
