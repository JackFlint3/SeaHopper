using MCDatapackCompiler.Compiler.Lexer.Trees.Token;
using MCDatapackCompiler.Compiler.Lexer.Trees.Token.General;
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Lexer
{
    public partial class StreamLexer
    {
        public class LexerToken : IExpression
        {
            readonly Token token;
            readonly string value;
            readonly bool definedValue = true;

            public LexerToken(Token token, string value)
            {
                this.token = token ?? throw new ArgumentNullException(nameof(token));
                this.value = value ?? throw new ArgumentNullException(nameof(value));
                TokenDiagram.RegisterToken(token.GetType(), value);
            }

            public LexerToken(Token token) : this(token, token.Key ?? "") { definedValue = false; }

            public Token Token { get => token; }
            public string Value { get => value; }

            public override bool Equals(object? obj)
            {
                if (obj != null && obj is LexerToken right)
                {
                    LexerToken left = this;

                    Type lTokenType = left.Token.GetType();
                    Type rTokenType = right.Token.GetType();

                    if (lTokenType == rTokenType || lTokenType.IsSubclassOf(rTokenType) || rTokenType.IsSubclassOf(lTokenType))
                    {
                        if (left.definedValue && right.definedValue)
                        {
                            if (left.Value.Equals(right.Value)) return true;
                        }
                        else return true;
                    }
                }
                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator == (LexerToken? left, LexerToken? right)
            {
                if (ReferenceEquals(left, right)) return true;
                else if (ReferenceEquals(left, null)) return false;
                else if (ReferenceEquals(right, null)) return false;
                else return left.Equals(right);
            }

            public static bool operator != (LexerToken? left, LexerToken? right)
            {
                return !(left == right);
            }

            public override string ToString()
            {
                if (this.Value == null)
                    return this.GetType().Name;
                else
                    return this.GetType().Name + "(" + this.Value.ToString() + ")";
            }

            public string Build()
            {
                if (!string.IsNullOrEmpty(this.Value)) return this.Value.ToString();
                else return "";
            }

            public string Build(string prefix)
            {
                return prefix + " " + this.Build();
            }
        }
    }
}
