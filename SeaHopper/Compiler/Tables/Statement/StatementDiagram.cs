using SeaHopper.Compiler.Lexer;
using System.Reflection;
using Patterns = SeaHopper.Compiler.Pattern.Patterns;
using SeaHopper.Compiler.Parser.Trees.Syntax.General;
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler.Parser.Trees.Syntax
{
    public partial class StatementDiagram
    {

        static StatementDiagram()
        {
            treeNodes = new Dictionary<Type, Statement>();
        }


        private static StatementDiagram? tree = null;
        public static StatementDiagram FetchDiagram()
        {
            if (tree == null) 
            {
                var pattern = RetrieveByType(typeof(Unspecific.Document));
                tree = new StatementDiagram(pattern); 
            }
            return tree;
        }


        private static Dictionary<Type, Statement> treeNodes;

        /// <summary>
        /// Registers an instance of statement by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="statement"></param>
        internal static void RegisterByType(Type type, Statement statement)
        {
            treeNodes.Add(type, statement);
        }

        /// <summary>
        /// Retrieves or creates an instance for a Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal static Statement RetrieveByType(Type type)
        {
            if (!treeNodes.ContainsKey(type))
            {
                ConstructorInfo? ctor = type.GetConstructor(Array.Empty<Type>());
                if (ctor != null)
                {
                    Statement statement = (Statement)ctor.Invoke(Array.Empty<object>());
                    treeNodes[type] = statement;
                    return statement;
                }
                else throw new Exception("Could not find parameterless contstructor for type: " + type);
            }
            else return treeNodes[type];
        }

        /// <summary>
        /// Retrieves all Instances of inherting classes for a Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static IReadOnlyList<IMatchable<StreamLexer.LexerToken>> GetSubclassesForType(Type type)
        {
            List<IMatchable<StreamLexer.LexerToken>> matchables = new List<IMatchable<StreamLexer.LexerToken>>();

            var assembly = Assembly.GetAssembly(type);
            if (assembly != null)
            {
                var assemblyTypes = assembly.GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(type));

                if (assemblyTypes == null) return matchables;
                foreach (Type t in assemblyTypes)
                {
                    Statement statement = RetrieveByType(t);
                    matchables.Add(statement);
                }
            }

            return matchables;
        }

        private readonly IMatchable<StreamLexer.LexerToken> head;

        private StatementDiagram(IMatchable<StreamLexer.LexerToken> treeHead) 
        {
            this.head = treeHead;
        }

        public bool Accept(IExpressionBuilder<StreamLexer.LexerToken> expressionBuilder)
        {
            return this.head.Match(expressionBuilder);
        }

        public override string ToString()
        {
            string? str = head.ToString();
            if (str != null) return str;
            else return "";
        }
    }
}
