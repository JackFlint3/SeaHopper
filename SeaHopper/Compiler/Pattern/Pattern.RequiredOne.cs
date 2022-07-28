
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler
{
    public abstract partial class Pattern<T>
    {
        public class RequiredOne : Pattern<T>
        {
            public RequiredOne(IReadOnlyList<IMatchable<T>> range) : base(range) { }

            /// <summary>
            /// Matches enumerator for exactly one match
            /// </summary>
            /// <param name="enumerator"></param>
            /// <param name="hasNext"></param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public override bool Match(IExpressionBuilder<T> enumerator)
            {

                foreach (var item in range)
                {
                    var sub = enumerator.Split();
                    if (item.Match(sub))
                    {
                        enumerator.Join(sub);
                        return true;
                    }
                }

                return false;
            }

            public override string ToString()
            {
                string str = "[";
                bool first = true;
                foreach (var item in range)
                {
                    if (first) first = false;
                    else str += "|";
                    str += item.ToString();
                }
                str += "]";
                return str;
            }
        }
    }
}
