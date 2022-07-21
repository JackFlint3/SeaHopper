using MCDatapackCompiler.Compiler.Builder;
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Parser
{
    internal partial class Submarine<T> where T : IBuildable
    {
        private class Layer
        {
            protected internal IExpressionProvider<T> expressionProvider;
            protected internal List<IBuildable> expressions = new List<IBuildable>();
            protected internal int depth;

            protected internal Layer() { }
            public Layer(IExpressionProvider<T> expressionProvider, int depth)
            {
                this.expressionProvider = expressionProvider ?? throw new ArgumentNullException(nameof(expressionProvider));
                this.depth = depth;
            }

            public IExpressionProvider<T> ExpressionProvider => expressionProvider;

            public List<IBuildable> Expressions => expressions;
        }
    }
}
