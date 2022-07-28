
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler
{
    public abstract partial class Pattern<T>
    {
        public class RequiredAll : Pattern<T>
        {
            public RequiredAll(IReadOnlyList<IMatchable<T>> range) : base(range) {  }

            /// <summary>
            /// Matches enumerator to match exactly all
            /// </summary>
            /// <param name="enumerator"></param>
            /// <param name="hasNext"></param>
            /// <returns></returns>
            public override bool Match(IExpressionBuilder<T> enumerator)
            {
                foreach (var item in range)
                {
                    var sub = enumerator.Split();
                    if (!item.Match(sub)) return false;
                    enumerator.Join(sub);
                }
                return true;
            }

            public override string ToString()
            {
                string str = "[";
                bool first = true;
                foreach (var item in range)
                {
                    if (first) first = false;
                    else str += ",";
                    str += item.ToString();
                }
                str += "]";
                return str;
            }
        }
    }
}
