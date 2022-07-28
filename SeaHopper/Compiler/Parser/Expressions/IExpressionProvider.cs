using SeaHopper.Compiler.Builder;

namespace SeaHopper.Compiler.Trees.Expressions
{
    public interface IExpressionProvider<T>
    {
        Expression GetExpression(IReadOnlyList<IBuildable> children);
    }
}
