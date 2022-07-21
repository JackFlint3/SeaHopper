using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCDatapackCompiler.Compiler.Lexer;
using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;
using MCDatapackCompiler.Compiler.Parser.Trees.Syntax;
using MCDatapackCompiler.Compiler.Trees.Expressions;
using MCDatapackCompiler.Compiler.Builder;

namespace MCDatapackCompiler.Compiler.Parser
{
    public class StreamParser : IExpressionProvider<StreamLexer.LexerToken>
    {
        private StatementDiagram diagram;
        private StreamLexer lexer;

        public StreamParser(string file)
        {
            this.diagram = FetchDiagram();
            lexer = new StreamLexer(file);
        }

        public Expression GetExpression(IReadOnlyList<IBuildable> expressions)
        {
            return new StatementHolder(expressions);
        }

        public Expression Parse()
        {
            // Tokenize from file
            LinkedList<StreamLexer.LexerToken> tokens = new LinkedList<StreamLexer.LexerToken>();
            while (!lexer.EOF)
            {
                var next = lexer.Next();
                if (next != null) 
                    tokens.AddLast(next);
            }

            StreamLexer.LexerToken[] lexerArr = tokens.ToArray();
            Submarine<StreamLexer.LexerToken> submarine = new Submarine<StreamLexer.LexerToken>(lexerArr);

            // Parsing Tokens into a SyntaxTree by diving into the diagram
            // The Token-Stream describes the path
            submarine.Prepare(this);
            Expression abstractSyntaxTree;

            if (diagram.Accept(submarine))
            {
                abstractSyntaxTree = submarine.Collapse();
            }
            //TODO: Insert Document parsing error info here
            else throw new Exception("Failed to parse document."); 

            return abstractSyntaxTree;
        }
    }
}
