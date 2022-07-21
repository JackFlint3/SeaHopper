using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using System.Text;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Builder;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        /// <summary>
        /// Technically this is JSON but its not being validated as of RN
        /// </summary>
        public abstract class NBT : Unspecific
        {
            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in expressions)
                {
                    stringBuilder.Append(item.Build());
                }

                ValueHolder valueHolder = new ValueHolder(stringBuilder.ToString());
                return valueHolder;
            }

            public class SNBT : NBT
            {
                public override Pattern<LexerToken> Pattern =>
                    Pattern = Patterns.All(new()
                        {
                            Patterns.One(new()
                            {
                                Patterns.All(new()
                                {
                                    Patterns.Symbols["{"],
                                    Patterns.Symbols["}"]
                                }),
                                Patterns.All(new()
                                {
                                    Patterns.Symbols["{"],
                                    Patterns.Many(Patterns.All(new()
                                    {
                                        Patterns.Literals.StringLiteral,
                                        Patterns.Operators[":"],
                                        Patterns.One(new()
                                        {
                                            Patterns.Literals.StringLiteral,
                                            Patterns.Literals.Number,
                                            Patterns.One(new()
                                            {
                                                Patterns.Keywords["true"],
                                                Patterns.Keywords["false"]
                                            }),
                                            Patterns.Keywords["null"],
                                            Patterns.One(new()
                                            {
                                                Patterns.All(new()
                                                {
                                                    Patterns.Symbols["["],
                                                    Patterns.Symbols["]"]
                                                }),
                                                Patterns.All(new()
                                                {
                                                    Patterns.Symbols["["],
                                                    Patterns.Many(
                                                        Patterns.One(new()
                                                        {
                                                            RetrieveByType(typeof(StringSafeSNBT)),
                                                            Patterns.Literals.StringLiteral,
                                                            Patterns.Literals.Number,
                                                        }), Patterns.Symbols[","]),
                                                    Patterns.Symbols["]"]
                                                })
                                            })
                                        })
                                    }), Patterns.Symbols[","]),
                                    Patterns.Symbols["}"]
                                })
                            })
                        });
                
            }

            private class StringSafeSNBT : SNBT
            {
                public override bool Match(IExpressionBuilder<LexerToken> builder)
                {
                    return Pattern.Match(builder);
                }

                public override string ToString()
                {
                    return "<SNBT>";
                }
            }

            public class Tag : NBT
            {

            }

            public class Path : NBT
            {
                public override Pattern<LexerToken> Pattern =>
                    Pattern = Patterns.All(new()
                        {
                            Patterns.Many(
                                Patterns.One(new()
                                {
                                    RetrieveByType(typeof(StringSafeSNBT)),
                                    Patterns.Literals.StringLiteral,
                                    Patterns.Literals.NameLiteral,
                                    Patterns.Literals.Number,
                                    Patterns.Symbols["."],
                                    Patterns.All(new()
                                    {
                                        Patterns.Symbols["["],
                                        Patterns.One(new()
                                        {
                                            RetrieveByType(typeof(StringSafeSNBT)),
                                            Patterns.Literals.Number
                                        }),
                                        Patterns.Symbols["]"]
                                    })
                                })
                            )
                        });
                
            }
        }
    }
}
