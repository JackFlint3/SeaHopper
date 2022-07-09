using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler
{
    public interface IMatchable<T>
    {
        bool Match(IExpressionBuilder<T> enumerator);
    }
}