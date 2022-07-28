
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler
{
    public partial class Pattern<T> //This Type 'T' is overloaded and not used in this context
    {
        public class Single : IMatchable<T>
        {
            private readonly T value;

            public Single(T value)
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                this.value = value;
            }

            public bool Match(IExpressionBuilder<T> enumerator)
            {
                if (this.value == null) throw new InvalidOperationException("Single cannot contain null");
                T val;
                try
                {
                    val = enumerator.Current;
                }
                catch (Exception)
                {
                    // TODO: LOG TO ERROR
                    return false;
                }

                if (val == null) throw new InvalidOperationException("ExpressionBuilder cannot provide null");

                if (value.Equals(val))
                {
                    enumerator.MoveNext();
                    return true;
                }
                else return false;
            }

            public override string ToString()
            {
                if (value != null)
                    return "'" + value.ToString() + "'";
                else return "''";
            }
        }
    }
}
