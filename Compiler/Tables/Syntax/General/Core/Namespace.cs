using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Pattern;
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class GeneralContext
    {
        public class Namespace : GeneralContext
        {
            public override Expression GetExpression(IReadOnlyList<IExpression> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, prefix) => {
                    string str = "";
                    if (expressions.Count == 0) return str;

                    string namespaceName = expressions[1].Build();
                    string namespaceDir = prefix + "/data/" + namespaceName;
                    string functionsDir = namespaceDir + "/functions";

                    Directory.CreateDirectory(functionsDir);
                    List<string> loadFunctions = new List<string>();
                    List<string> tickFunctions = new List<string>();

                    for (int iFun = 3; iFun < expressions.Count - 1; iFun++)
                    {
                        var fun = (StatementHolder)expressions[iFun];
                        // Replace initial dataPack Name with project name
                        string functionName = fun.Children[fun.Children.Count - 2].Build();
                        string fileTarget = functionsDir + "/" + functionName + ".mcfunction";
                        string generated = fun.Build();

                        if (fun.Children.Count > 3)
                            for (int iTag = 0; iTag < fun.Children.Count - 3; iTag++)
                            {
                                var tag = (StatementHolder)fun.Children[iTag];
                                switch (tag.Children[1].Build())
                                {
                                    case "tick": tickFunctions.Add(namespaceName + ":" + functionName); break;
                                    case "load": loadFunctions.Add(namespaceName + ":" + functionName); break;
                                    default: throw new Exception("Unknown function tag");
                                }
                            }
                        

                        File.WriteAllText(fileTarget, generated);

                        str += "output file : " + fileTarget + " >>>\n";
                        str += generated + "\n";
                    }

                    string functionTagsDir = prefix + "/data/minecraft/tags/functions";
                    Directory.CreateDirectory(functionTagsDir);

                    #region Generate Load Tags
                    string load = "{\"values\":[\n";
                    foreach (var item in loadFunctions)
                    {
                            load += "\t\"" + item + "\"\n";
                    }
                    load += "]}";
                    File.WriteAllText(functionTagsDir + "/load.json", load);
                    str += "output file : " + functionTagsDir + "/load.json >>>\n";
                    str += load + "\n\n";
                    #endregion

                    #region Generate Tick Tags
                    string tick = "{\"values\":[\n";
                    foreach (var item in tickFunctions)
                    {
                        tick += "\t\"" + item + "\"\n";
                    }
                    tick += "]}";
                    File.WriteAllText(functionTagsDir + "/tick.json", tick);
                    str += "output file : " + functionTagsDir + "tick.json >>>\n";
                    str += tick + "\n\n";
                    #endregion

                    return str;
                });
                return holder;
            }

            public override Pattern<LexerToken> Pattern =>
                Patterns.All(new()
                {
                    Patterns.Keywords["namespace"],
                    Patterns.Literals.NameLiteral,
                    Patterns.Symbols["{"],
                    Patterns.Many(RetrieveByType(typeof(Function))),
                    Patterns.Symbols["}"]
                });
        }
    }
}
