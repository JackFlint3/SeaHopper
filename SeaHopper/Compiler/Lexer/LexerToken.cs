using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Lexer.Trees.Token;
using SeaHopper.Compiler.Lexer.Trees.Token.General;

namespace SeaHopper.Compiler.Lexer
{
    public partial class StreamLexer
    {
        public class LexerToken : IBuildable
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

            public string Build(Builder.Context.BuildContext context)
            {
                string prefix = "";
                if (context != null) prefix = context.Data["prefix"];
                if (string.IsNullOrEmpty(prefix)) return this.Build();
                else return prefix + " " + this.Build();
            }
        }
    }
}
