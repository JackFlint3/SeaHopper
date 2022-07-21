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
                public class Vector2 : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                            RetrieveByType(typeof(Relative)),
                            RetrieveByType(typeof(Local))
                        });


                    public class Relative : Vector2
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Any(new(){ Patterns.Operators["~"] }),
                                RetrieveByType(typeof(Brigadier.Numerics.Float)),
                                Patterns.Any(new(){ Patterns.Operators["~"] }),
                                RetrieveByType(typeof(Brigadier.Numerics.Float))
                            });
                    }
                    public class Local : Vector2
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new() {
                                Patterns.Operators["^"],
                                RetrieveByType(typeof(Brigadier.Numerics.Float)),
                                Patterns.Operators["^"],
                                RetrieveByType(typeof(Brigadier.Numerics.Float))
                            });

                    }
                }
            }
        }
    }
}
