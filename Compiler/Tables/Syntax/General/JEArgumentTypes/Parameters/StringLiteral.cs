﻿using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
public abstract partial class JEArgumentTypes
        {
            public abstract partial class Parameters
            {
                public class StringLiteral : JEArgumentTypes
                {
                    public override Pattern<LexerToken> Pattern =>
                        Patterns.One(new() {
                        Patterns.Literals.StringLiteral
                        });
                }
            }
        }
    }
}
