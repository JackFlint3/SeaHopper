using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Int_Range : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new()
                        {
                            RetrieveByType(typeof(Brigadier.Numerics.Int)),
                            RetrieveByType(typeof(RangeFromTo)),
                            RetrieveByType(typeof(RangeFrom)),
                            RetrieveByType(typeof(RangeTo))
                        });
                    public class RangeFromTo : Int_Range
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                RetrieveByType(typeof(Brigadier.Numerics.Int)),
                                Patterns.Symbols["."],
                                Patterns.Symbols["."],
                                RetrieveByType(typeof(Brigadier.Numerics.Int))
                            });
                    }
                    public class RangeFrom : Int_Range
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                RetrieveByType(typeof(Brigadier.Numerics.Int)),
                                Patterns.Symbols["."],
                                Patterns.Symbols["."]
                            });
                    }
                    public class RangeTo : Int_Range
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Symbols["."],
                                Patterns.Symbols["."],
                                RetrieveByType(typeof(Brigadier.Numerics.Int)),
                            });
                    }
                }
            }
        }
    }
}
