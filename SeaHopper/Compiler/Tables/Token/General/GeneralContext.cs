namespace SeaHopper.Compiler.Lexer.Trees.Token
{
    public abstract class GeneralContext
    {
        public class StringLiteral : General.Token
        {
            public override string? Key => null;
            public override bool MatchesKey(string key)
            {
                if (key.Length < 2) return false;
                if (key[0] != '\"') return false;
                for (int iChar = 1; iChar < key.Length - 1; iChar++)
                {
                    if (key[iChar] == '\"') return false;
                }
                if (key[key.Length - 1] != '\"') return false;

                return true;
            }

            public override bool KeyContains(string sequence)
            {
                if (sequence.Length == 0) return true;
                if (sequence[0] != '\"') return false;
                bool completeLiteral = false;
                for (int iChar = 1; iChar < sequence.Length; iChar++)
                {
                    if (completeLiteral) throw new Exception("Sequence overextends qualified StringLiteral :'" + sequence + "'");
                    if (sequence[iChar] == '\"')
                    {
                        completeLiteral = true;
                    }
                }
                
                return true;
            }
        }

        public class NameLiteral : General.Token
        {
            public override string? Key => null;
            public override bool MatchesKey(string key)
            {
                foreach (char c in key)
                {
                    if (!(char.IsDigit(c) || char.IsLetter(c) || c == '_')) return false;
                }

                return true;
            }

            public override bool KeyContains(string sequence)
            {
                return MatchesKey(sequence);
            }
        }


        public class Number : NameLiteral
        {
            public override bool MatchesKey(string key)
            {
                foreach (char c in key)
                {
                    if (!(char.IsDigit(c))) return false;
                }

                return true;
            }
        }

        public class Keyword : NameLiteral
        {
            public override bool MatchesKey(string key)
            {
                foreach (var item in TokenDiagram.GetTokenNames(this.GetType()))
                {
                    if (item == key) return true;
                }

                return false;
            }

            public override bool KeyContains(string sequence)
            {
                foreach (var item in TokenDiagram.GetTokenNames(this.GetType()))
                {
                    if (item.StartsWith(sequence)) return true;
                }

                return false;
            }
        }

        public class Identifier : Keyword
        {

        }

        public class Symbol : General.Token
        {
            public override string? Key => null;

            public override bool MatchesKey(string key)
            {
                foreach (var item in TokenDiagram.GetTokenNames(this.GetType()))
                {
                    if (item == key) return true;
                }

                return false;
            }

            public override bool KeyContains(string sequence)
            {
                foreach (var item in TokenDiagram.GetTokenNames(this.GetType()))
                {
                    if (item.StartsWith(sequence)) return true;
                }

                return false;
            }
        }

        public class Operator : General.Token
        {
            public override string? Key => null;

            public override bool MatchesKey(string key)
            {
                foreach (var item in TokenDiagram.GetTokenNames(this.GetType()))
                {
                    if (item == key) return true;
                }

                return false;
            }

            public override bool KeyContains(string sequence)
            {
                foreach (var item in TokenDiagram.GetTokenNames(this.GetType()))
                {
                    if (item.StartsWith(sequence)) return true;
                }

                return false;
            }
        }

        public class Whitespace : General.Token
        {
            public override string? Key => null;
            public override bool MatchesKey(string key)
            {
                foreach (char c in key)
                {
                    if (!(c == ' ' || c == '\t' || c == 32)) return false;
                }

                return true;
            }

            public override bool KeyContains(string sequence)
            {
                return MatchesKey(sequence);
            }
        }

        public class Comment : General.Token
        {
            public override string? Key => null;
            public override bool MatchesKey(string key)
            {
                if (key.StartsWith("//"))
                {
                    if (key.EndsWith("\n") || key.EndsWith("\r\n")) return true;
                } else if (key.StartsWith("/*"))
                {
                    if (key.EndsWith("*/")) return true;
                }
                return false;
            }

            public override bool KeyContains(string sequence)
            {
                if (sequence.StartsWith("/"))
                {
                    if (sequence.Length == 1) return true;
                    if (sequence[1] == '/')
                    {
                        if (sequence.EndsWith("\n") || sequence.EndsWith("\r\n")) return true;
                        else if (sequence.Contains("\n") || sequence.Contains("\r\n")) return false;
                        else return true;
                    }
                    else if (sequence[1] == '*')
                    {
                        if (sequence.EndsWith("*/")) return true;
                        else if (sequence.Contains("*/")) return false;
                        else return true;
                    }
                }
                return false;
            }
        }

        public class NewLine : Whitespace
        {
            public override string? Key => null;

            public override bool MatchesKey(string key)
            {
                if (key == "\r\n") return true;
                if (key == "\n") return true;

                return false;
            }

            public override bool KeyContains(string sequence)
            {
                if (sequence == "\r") return true;
                if (sequence == "\n") return true;

                return false;
            }
        }
    }
}
