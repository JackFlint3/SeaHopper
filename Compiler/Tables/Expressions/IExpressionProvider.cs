namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public interface IExpressionProvider<T>
    {
        Expression GetExpression(IReadOnlyList<IExpression> children);
    }
}
