using MCDatapackCompiler.Compiler.Trees.Expressions;

namespace MCDatapackCompiler.Compiler.Trees.Expressions
{
    public class ValueHolder : Expression
    {
        public object Value { get; set; }

        public ValueHolder(object value, Func<IReadOnlyList<IExpression>, string, string> printer) : base(printer)
        {
            Value = value;
        }

        public ValueHolder(object value) :
            base(
                (expressions, prefix) =>
                {
                    string? str;
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
            return base.Build("");
        }
    }
}
