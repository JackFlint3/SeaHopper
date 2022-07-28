using SeaHopper.Compiler.Lexer.Trees.Token;
using SeaHopper.Compiler.Lexer.Trees.Token.General;
using System.Text;
using static SeaHopper.Compiler.Lexer.Trees.Token.GeneralContext;

namespace SeaHopper.Compiler.Lexer
{
    public partial class StreamLexer
    {
        private class LexerInstruction
        {

            readonly Token token;
            readonly bool qualify;
            readonly bool strict;
            bool passing = true;
            readonly LexerInstruction[] subInstructions;

            public LexerInstruction(Token token, bool qualify, bool strict)
            {
                this.token = token ?? throw new ArgumentNullException(nameof(token));
                this.qualify = qualify;
                this.strict = strict;

                subInstructions = new LexerInstruction[0];
            }

            public LexerInstruction(Token token, bool qualify, bool strict, LexerInstruction[] subInstructions) : this(token, qualify, strict)
            {
                this.subInstructions = subInstructions ?? throw new ArgumentNullException(nameof(subInstructions));
            }

            public Token Token { get => token; }
            public bool Qualify { get => qualify; }
            public bool Passing { get => passing; set => passing = value; }
            public bool Strict => strict;
            public LexerInstruction[] SubInstructions => subInstructions;
        }


        private FileIterator Reader;
        private LexerInstruction[] instructions;
        string file;

        public StreamLexer(string file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
            this.Reader = new FileIterator(file);
            instructions = new LexerInstruction[]
            {
                new LexerInstruction(new Whitespace(), false, false),
                new LexerInstruction(new NewLine(), false, true),

                new LexerInstruction(new StringLiteral(), true, true),

                new LexerInstruction(new NameLiteral(), true, false, new LexerInstruction[]{
                    new LexerInstruction(new Number(), true, false),
                    new LexerInstruction(new Keyword(), true, true, new LexerInstruction[]
                    {
                        new LexerInstruction(new Identifier(), true, true),
                    }),
                }),

                new LexerInstruction(new Symbol(), true, true),
                new LexerInstruction(new Operator(), true, true),
                new LexerInstruction(new Comment(), false, true)
            };
        }

        public bool EOF => Reader.EOF;


        public LexerToken? Next()
        {
            StringBuilder nextIdentifier = new();

            bool hasNext = false;
            bool readerEnd = false;
            bool movedOnce = false;
            LexerInstruction? candidate = null;
            string current = "";

            while (!hasNext && !readerEnd)
            {
                char c = Reader.Current;
                nextIdentifier.Append(c);
                current = nextIdentifier.ToString();


                #region Solve Ambiguosity
                int ambiguosity = Ambiguosity(current, instructions);

                string next = current;
                while (ambiguosity > 1)
                {
                    readerEnd = !Reader.MoveNext();
                    if (readerEnd) break;
                    else movedOnce = true;

                    c = Reader.Current;
                    nextIdentifier.Append(c);
                    current = next;
                    next = nextIdentifier.ToString();
                    ambiguosity = Ambiguosity(next, instructions);
                }
                if (ambiguosity == 1) current = next;
                #endregion


                candidate = GetInstructionHead(current);


                #region Cycle to most specific
                //next = current;
                while (
                    (!candidate.Token.MatchesKey(current) && candidate.Strict == true) || 
                    (candidate.Token.MatchesKey(next) && candidate.Strict == false))
                {
                    if (readerEnd) throw new Exception("Could not resolve to token: '" + current + "'");
                    readerEnd = !Reader.MoveNext();
                    if (readerEnd) break;
                    else movedOnce = true;

                    c = Reader.Current;
                    nextIdentifier.Append(c);
                    current = next;
                    next = nextIdentifier.ToString();
                }
                #endregion


                if (candidate.Qualify)
                {
                    if (candidate.Strict == false)
                    {
                        foreach (var item in candidate.SubInstructions)
                        {
                            if (item.Token.MatchesKey(current)) candidate = item;
                        }
                    }
                    hasNext = true;
                } 
                else
                {
                    candidate = null;
                    nextIdentifier.Clear();
                    if (!movedOnce && !readerEnd) readerEnd = !Reader.MoveNext();
                    movedOnce = false;
                }

                ResetInstructions(instructions);
            }

            if (hasNext && !movedOnce) Reader.MoveNext();
            if (candidate == null) return null;
            else
            {
                return new LexerToken(candidate.Token, current);
            }
        }

        private LexerInstruction GetInstructionHead(string key)
        {
            var query = from LexerInstruction inst in instructions
                        where (inst.Passing == true && inst.Token.KeyContains(key))
                        select inst;
            if (query.Count() > 1) throw new Exception("Expected unambiguos but got ambiguos");
            return query.First();
        }

        private int Ambiguosity(string sequence, LexerInstruction[] instructions)
        {
            int count = 0;

            var query = from LexerInstruction inst in instructions
                        where inst.Passing == true
                        select inst;

            foreach (var item in query)
            {
                if (item.Token.KeyContains(sequence)) count++;
                else item.Passing = false;
            }

            return count;
        }

        private void ResetInstructions(LexerInstruction[] instructions)
        {
            foreach (var item in instructions)
            {
                item.Passing = true;
                ResetInstructions(item.SubInstructions);
            }
        }
    }
}
