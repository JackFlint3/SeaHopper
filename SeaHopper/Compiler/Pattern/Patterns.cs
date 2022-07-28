using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeaHopper.Compiler.Lexer.StreamLexer;

namespace SeaHopper.Compiler.Pattern
{
    public static class Patterns
    {
        public class PatternTable<T> where T : Lexer.Trees.Token.General.Token, new()
        {
            Dictionary<string, Pattern<LexerToken>.Single> patterns = new Dictionary<string, Pattern<LexerToken>.Single>();

            public Pattern<LexerToken>.Single this[string s]
            {
                get
                {
                    if (!patterns.ContainsKey(s))
                    {
                        patterns[s] = new(new(new T(), s));
                    }
                    return patterns[s];
                }
            }

            public bool ContainsKey(string key) => patterns.ContainsKey(key);
            public static readonly PatternTable<T> Empty = new PatternTable<T>();
        }


        public static Pattern<LexerToken> All(List<IMatchable<LexerToken>> matchables) => new Pattern<LexerToken>.RequiredAll(matchables);
        public static Pattern<LexerToken> One(List<IMatchable<LexerToken>> matchables) => new Pattern<LexerToken>.RequiredOne(matchables);
        public static Pattern<LexerToken> Any(List<IMatchable<LexerToken>> matchables) => new Pattern<LexerToken>.Optional(matchables);
        public static IMatchable<LexerToken> Many(IMatchable<LexerToken> matchable, Pattern<LexerToken>.Single seperator) => new Pattern<LexerToken>.Repeater(matchable, seperator);
        public static IMatchable<LexerToken> Many(IMatchable<LexerToken> matchable) => new Pattern<LexerToken>.Repeater(matchable);

        public static class Literals
        {
            public static readonly Pattern<LexerToken>.Single Identifier = new(new(new Lexer.Trees.Token.GeneralContext.Identifier()));
            public static readonly Pattern<LexerToken>.Single Number = new(new(new Lexer.Trees.Token.GeneralContext.Number()));
            public static readonly Pattern<LexerToken>.Single StringLiteral = new(new(new Lexer.Trees.Token.GeneralContext.StringLiteral()));
            public static readonly Pattern<LexerToken>.Single NameLiteral = new(new(new Lexer.Trees.Token.GeneralContext.NameLiteral()));
            //public static readonly Pattern<LexerToken>.Single UUID = new(new(new Lexer.Trees.Token.GeneralContext.NameLiteral()));
        }

        public static readonly PatternTable<Lexer.Trees.Token.GeneralContext.Keyword> Keywords = new();
        public static readonly PatternTable<Lexer.Trees.Token.GeneralContext.Symbol> Symbols = new();
        public static readonly PatternTable<Lexer.Trees.Token.GeneralContext.Operator> Operators = new();
    }
}
