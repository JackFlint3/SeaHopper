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
                    public class Float : Numerics
                    {
                        // TODO: Constraint max abs 3.4*19^38
                        public override Pattern<LexerToken> Pattern =>
                                Patterns.All(new()
                                {
                                    Patterns.Any(new()
                                    {
                                        Patterns.Operators["-"]
                                    }),
                                    Patterns.One(new() {
                                        Patterns.All(new()
                                        {
                                            Patterns.Literals.Number,
                                            Patterns.Any(new()
                                            {
                                                Patterns.Symbols["."],
                                                Patterns.Literals.Number
                                            })
                                        }),
                                        Patterns.All(new()
                                        {
                                            Patterns.All(new()
                                            {
                                                Patterns.Symbols["."],
                                                Patterns.Literals.Number
                                            })
                                        })
                                    })
                                });

                            public class Double : Brigadier
                            {
                                // TODO: Constraint max abs 1.8*10^308
                        
                            }
                    }
                }
            }
        }
    }
}
