﻿using MCDatapackCompiler.Compiler.Pattern;
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
                public class Particle : Minecraft
                {
                    // TODO: Implement particle support
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            RetrieveByType(typeof(Resource_Location))
                        });
                }
            }
        }
    }
}
