using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler
{
    public interface IMatchable<T>
    {
        bool Match(IExpressionBuilder<T> enumerator);
    }
}