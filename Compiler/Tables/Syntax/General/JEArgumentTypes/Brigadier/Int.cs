using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext : Statement
    {
        public abstract partial class JEArgumentTypes : GeneralContext
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
