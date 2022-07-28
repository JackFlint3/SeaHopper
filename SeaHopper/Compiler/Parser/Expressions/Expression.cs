using SeaHopper.Compiler.Builder;

namespace SeaHopper.Compiler.Trees.Expressions
{
    public abstract class Expression : IBuildable
    {
        protected internal readonly Func<IReadOnlyList<IBuildable>,Builder.Context.BuildContext, string> printer;

        protected Expression(Func<IReadOnlyList<IBuildable>,Builder.Context.BuildContext, string> printer)
        {
            this.printer = printer;
        }

        public virtual string Build() => printer(Array.Empty<IBuildable>(), null);
        public virtual string Build(Builder.Context.BuildContext context) => printer(Array.Empty<IBuildable>(), context);
    }
}
