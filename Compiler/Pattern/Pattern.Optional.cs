
using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler
{

    public abstract partial class Pattern<T>
    {
        public class Optional : Pattern<T>
        {
            public Optional(IReadOnlyList<IMatchable<T>> range) : base(range) { }
            public override bool Match(IExpressionBuilder<T> enumerator)
            {
                if (enumerator == null) throw new ArgumentNullException(nameof(enumerator));

                foreach (IMatchable<T> item in range)
                {
                    var sub = enumerator.Split();
                    if (item.Match(sub))
                    {
                        enumerator.Join(sub);
                    }
                }
                return true;
            }

            public override string ToString()
            {
                string str = "(";
                bool first = true;
                foreach (var item in range)
                {
                    if (first) first = false;
                    else str += "|";
                    str += item.ToString();
                }
                str += ")";
                return str;
            }
        }
    }
}
