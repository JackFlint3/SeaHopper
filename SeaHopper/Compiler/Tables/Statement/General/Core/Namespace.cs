using static SeaHopper.Compiler.Lexer.StreamLexer;
using static SeaHopper.Compiler.Parser.Trees.Syntax.StatementDiagram;
using SeaHopper.Compiler.Pattern;
using SeaHopper.Compiler.Trees.Expressions;
using SeaHopper.Compiler.Builder;

namespace SeaHopper.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public class Namespace : Unspecific
        {
            public override Expression GetExpression(IReadOnlyList<IBuildable> expressions)
            {
                var holder = new StatementHolder(expressions, (expressions, context) => {
                    string str = "";
                    if (expressions.Count == 0) return str;

                    string namespaceName = expressions[1].Build(context);
                    Builder.Context.Global.Namespace @namespace = context.Datapack[namespaceName];

                    // Functions are starting from not-including the 3rd to the last child, therefore 3 and -1
                    for (int iFun = 3; iFun < expressions.Count - 1; iFun++)
                    {
                        var fun = (StatementHolder)expressions[iFun];
                        // Replace initial dataPack Name with project name
                        string functionName = fun.Children[fun.Children.Count - 2].Build(context);
                        // Function holds StatementHolder for its body. Refer to Function.cs Pattern
                        StatementHolder functionBody = (StatementHolder)fun.Children[fun.Children.Count - 1];

                        // TODO: Add function file-origin and local using-directives here
                        Builder.Context.Global.Function function = new Builder.Context.Global.Function(functionName, functionBody);
                        @namespace.AddFunction(function);

                        if (fun.Children.Count > 3)

                            // Last 3 Expression in Function are always present
                            for (int iTag = 0; iTag < fun.Children.Count - 3; iTag++)
                            {
                                var tag = (StatementHolder)fun.Children[iTag];
                                string name = tag.Children[tag.Children.Count - 2].Build(context);
                                string refNamespace;
                                if (tag.Children.Count == 3)
                                {
                                    refNamespace = @namespace.Identifier;
                                } else
                                {
                                    refNamespace = tag.Children[1].Build(context);
                                }

                                context.Datapack[refNamespace].AddTaggedFunction(name, function);
                            }
                    }


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
