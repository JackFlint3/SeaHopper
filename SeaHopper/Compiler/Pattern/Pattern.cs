
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler
{
    public abstract partial class Pattern<T> : IMatchable<T>
    {
        readonly IReadOnlyList<IMatchable<T>> range;

        public Pattern(IReadOnlyList<IMatchable<T>> range)
        {
            this.range = range ?? throw new ArgumentNullException(nameof(range));
        }

        public abstract bool Match(IExpressionBuilder<T> enumerator);

        public override string ToString()
        {
            string str = "[ ";
            int n = 0;
            foreach (var item in range)
            {
                if (n++ == 0)
                    str += n + " : '"+ item.ToString() + "'";
                else
                    str += ", " + n + " : '" + item.ToString() + "'";
            }
            str += " ]";
            return str;
        }
    }   
}
