namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public interface IExpressionBuilder<T> : IEnumerator<T>
    {
        public void Prepare(IExpressionProvider<T> provider);
        public void Discard();
        public Expression Collapse();
        public void Collect(Expression expression);
        public IExpressionBuilder<T> Split();
        public void Join(IExpressionBuilder<T> builder);
    }
}
