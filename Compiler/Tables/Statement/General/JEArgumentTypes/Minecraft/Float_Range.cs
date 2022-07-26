using MCDatapackCompiler.Compiler.Pattern;
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
                public class Float_Range : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new()
                        {
                            RetrieveByType(typeof(Brigadier.Numerics.Float)),
                            RetrieveByType(typeof(RangeFromTo)),
                            RetrieveByType(typeof(RangeFrom)),
                            RetrieveByType(typeof(RangeTo))
                        });
                    public class RangeFromTo : Float_Range
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                RetrieveByType(typeof(Brigadier.Numerics.Float)),
                                Patterns.Symbols["."],
                                Patterns.Symbols["."],
                                RetrieveByType(typeof(Brigadier.Numerics.Float))
                            });
                    }
                    public class RangeFrom : Float_Range
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                RetrieveByType(typeof(Brigadier.Numerics.Float)),
                                Patterns.Symbols["."],
                                Patterns.Symbols["."]
                            });
                    }
                    public class RangeTo : Float_Range
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new()
                            {
                                Patterns.Symbols["."],
                                Patterns.Symbols["."],
                                RetrieveByType(typeof(Brigadier.Numerics.Float)),
                            });
                    }
                }
            }
        }
    }
}
