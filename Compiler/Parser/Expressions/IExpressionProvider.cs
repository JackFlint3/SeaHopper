using MCDatapackCompiler.Compiler.Builder;

namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public interface IExpressionProvider<T>
    {
        Expression GetExpression(IReadOnlyList<IBuildable> children);
    }
}
