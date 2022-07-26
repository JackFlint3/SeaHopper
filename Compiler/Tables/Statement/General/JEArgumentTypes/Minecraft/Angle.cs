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
                public class Angle : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            RetrieveByType(typeof(Brigadier.Numerics.Float)),
                            RetrieveByType(typeof(RelAngle))
                        });
                    public class RelAngle : Angle
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Operators["~"],
                                Patterns.Any(new()
                                {
                                    RetrieveByType(typeof(Brigadier.Numerics.Float))
                                })
                            });
                    }

                }
            }
        }
    }
}
