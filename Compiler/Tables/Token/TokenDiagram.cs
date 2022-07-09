using System.Reflection;

namespace MCDatapackCompiler.Compiler.Lexer.Trees.Token
{
    public partial class TokenDiagram
    {
        #region Static


        private static Dictionary<Type, List<string>> stringToken = new Dictionary<Type, List<String>>();

        public static void RegisterToken(Type t, string value)
        {
            if (stringToken.ContainsKey(t))
            {
                if (stringToken[t].Contains(value)) return;
                stringToken[t].Add(value);
            } else
            {
                stringToken.Add(t, new List<string>());
                stringToken[t].Add(value);
            }
        }

        public static ICollection<string> GetTokenNames(Type t)
        {
            if (stringToken.ContainsKey(t)) return stringToken[t];
            else return Array.Empty<string>();
        }
        #endregion


        //private static List<TokenContainer> BuildPairsForType(Type type, int depth)
        //{
        //    List<TokenContainer> pairs = new List<TokenContainer>();
        //    var assembly = Assembly.GetAssembly(type);
        //    if (assembly != null)
        //    {
        //        var assemblyTypes = assembly.GetTypes()
        //            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(type));
        //        if (assemblyTypes == null) return pairs;
        //        foreach (Type t in assemblyTypes)
        //        {
        //            ConstructorInfo ctor = t.GetConstructor(Array.Empty<Type>());
        //            if (ctor != null)
        //            {
        //                General.Token token = (General.Token)ctor.Invoke(new object[0]);
        //                TokenContainer container = new TokenContainer(token, BuildPairsForType(t, depth + 1), depth);
        //                pairs.Add(container);
        //            }
        //        }
        //    }


        //    return pairs;
        //}



        //private List<TokenContainer> context;

        //public TokenDiagram()
        //{
        //    context = BuildPairsForType(typeof(General.Token), 1);
        //}


        //public List<General.Token> InIdentifier(string sequence)
        //{
        //    List<General.Token> tokens = new List<General.Token>();
        //    InIdentifier(sequence, context, tokens);
        //    return tokens;
        //}


        //private void InIdentifier(string sequence, List<TokenContainer> context, in List<General.Token> tokens)
        //{
        //    foreach (var item in context)
        //        if (item.token.KeyContains(sequence))
        //            tokens.Add(item.token);

        //    foreach (var item in context)
        //        InIdentifier(sequence, item.subTokens, tokens);
        //}


        ///// <summary>
        ///// Determines whether sequence is part of any identifier
        ///// </summary>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //public bool IsInIdentifier(string sequence)
        //{
        //    return IsInIdentifier(sequence, context);
        //}

        //private bool IsInIdentifier(string sequence, List<TokenContainer> context)
        //{
        //    foreach (var item in context)
        //        if (item.token.KeyContains(sequence))
        //            return true;

        //    foreach (var item in context)
        //        if (IsInIdentifier(sequence, item.subTokens))
        //            return true;

        //    return false;
        //}

        ///// <summary>
        ///// Attempts to retrieve TokenContainer at identifier
        ///// </summary>
        ///// <param name="identifier"></param>
        ///// <returns></returns>
        //public TokenContainer? GetElement(string identifier)
        //{
        //    TokenContainer? container;

        //    if (TryGetContainer(identifier, context, out container))
        //    {
        //        return container;
        //    }

        //    return null;
        //}

        //private bool TryGetContainer(string identifier, List<TokenContainer> context, out TokenContainer? container)
        //{
        //    foreach (var item in context)
        //        if (item.token.MatchesKey(identifier))
        //        {
        //            container = item;
        //            return true;
        //        }

        //    foreach (var item in context)
        //        if (TryGetContainer(identifier, item.subTokens, out container)) return true;

        //    container = null;
        //    return false;
        //}
    }
}
