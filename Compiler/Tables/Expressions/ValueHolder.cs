using MCDatapackCompiler.Compiler.Builder;

namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public class ValueHolder : Expression
    {
        public object Value { get; set; }

        public ValueHolder(object value, Func<IReadOnlyList<IBuildable>, Builder.Context.BuildContext, string> printer) : base(printer)
        {
            Value = value;
        }

        public ValueHolder(object value) :
            base(
                (expressions, context) =>
                {
                    string? str;
                    string prefix = null;
                    if (context != null) prefix = context.Data["prefix"];

                    if (string.IsNullOrEmpty(prefix))
                        str = "";
                    else str = prefix + " ";
                    if (value == null) return prefix + "";
                    else
                    {
                        str += value.ToString();
                        return str;
                    }
                })
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            return base.Build();
        }
    }
}
