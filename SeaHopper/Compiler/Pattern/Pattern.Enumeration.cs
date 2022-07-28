
using SeaHopper.Compiler.Trees.Expressions;

namespace SeaHopper.Compiler
{

    public abstract partial class Pattern<T>
    {
        public class Repeater : IMatchable<T>
        {
            Pattern<T> pattern;
            private IMatchable<T> matchable;
            private IMatchable<T>? seperator;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="matchable"></param>
            /// <param name="seperator"></param>
            public Repeater(IMatchable<T> matchable, IMatchable<T> seperator) 
            {
                this.matchable = matchable;
                this.seperator = seperator;
                pattern = new Pattern<T>.RequiredOne(new IMatchable<T>[]
                    {
                        new Pattern<T>.RequiredAll(new IMatchable<T>[]
                        {
                            matchable,
                            seperator,
                            this
                        }),
                        new Pattern<T>.RequiredAll(new IMatchable<T>[]
                        {
                            matchable
                        })
                    });
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="matchable"></param>
            public Repeater(IMatchable<T> matchable)
            {
                this.matchable = matchable;
                this.seperator = null;
                pattern = new Pattern<T>.RequiredOne(new IMatchable<T>[]
                    {
                        new Pattern<T>.RequiredAll(new IMatchable<T>[]
                        {
                            matchable,
                            this
                        }),
                        new Pattern<T>.RequiredAll(new IMatchable<T>[]
                        {
                            matchable
                        })
                    });
            }

            public bool Match(IExpressionBuilder<T> enumerator)
            {
                if (pattern == null) throw new InvalidOperationException();
                return pattern.Match(enumerator);
            }

            public override string ToString()
            {
                if (seperator == null)
                    return "{" + matchable.ToString() + "}";
                else
                    return "{" + matchable.ToString() + "," + this.seperator.ToString() + "}";
            }
        }
    }
}
