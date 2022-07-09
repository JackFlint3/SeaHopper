namespace MCDatapackCompiler.Compiler.Lexer.Trees.Token
{
    public class TokenContainer
    {
        public General.Token token;
        public List<TokenContainer> subTokens;
        public bool HasSubtoken => subTokens.Count > 0;
        public int depth;

        public TokenContainer(General.Token token, List<TokenContainer> subTokens, int depth)
        {
            this.token = token ?? throw new ArgumentNullException(nameof(token));
            this.subTokens = subTokens ?? throw new ArgumentNullException(nameof(subTokens));
            this.depth = depth;
        }
    }
    
}
