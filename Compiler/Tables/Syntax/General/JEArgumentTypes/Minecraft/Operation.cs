using MCDatapackCompiler.Compiler.Pattern;
using static MCDatapackCompiler.Compiler.Lexer.StreamLexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific
    {
        public abstract partial class JEArgumentTypes
        {
            public abstract partial class Minecraft
            {
                public class Operation : Minecraft
                {
                    public override Pattern<LexerToken> Pattern =>
                        new Pattern<LexerToken>.RequiredOne(GetSubclassesForType(this.GetType()));

                    public class Assignment : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.One(new(){
                                Patterns.Operators["="]
                            });
                    }
                    public class Addition : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators["+"],
                                Patterns.Operators["="]
                            });
                    }
                    public class Subtraction : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators["-"],
                                Patterns.Operators["="]
                            });
                    }
                    public class Multiplication : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators["+"],
                                Patterns.Operators["="]
                            });
                    }
                    public class FloorDivision : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators["/"],
                                Patterns.Operators["="]
                            });
                    }
                    public class Modulus : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators["%"],
                                Patterns.Operators["="]
                            });
                    }
                    public class Swapping : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators[">"],
                                Patterns.Operators["<"]
                            });
                    }
                    public class ChoosingMinimum : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators["<"]
                            });
                    }
                    public class ChoosingMaximum : Operation
                    {
                        public override Pattern<LexerToken> Pattern =>
                            Patterns.All(new(){
                                Patterns.Operators[">"]
                            });
                    }
                }
            }
        }
    }
}
