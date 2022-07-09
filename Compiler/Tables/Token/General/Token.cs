namespace MCDatapackCompiler.Compiler.Lexer.Trees.Token.General
{
    public abstract class Token
    {
        public abstract string? Key { get; }
        public virtual bool MatchesKey(string key)
        {
            return key == Key;
        }

        public virtual bool KeyContains(string sequence)
        {
            if (Key == null && sequence == null) return true;
            else if (Key == null || sequence == null) return false;
            else return Key.StartsWith(sequence);
        }
    }
}
