using MCDatapackCompiler.Compiler.Trees.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MCDatapackCompiler.Compiler.Trees.Expressions.Expression;

namespace MCDatapackCompiler.Compiler.Parser
{
    internal class Submarine<T> : IExpressionBuilder<T> where T : IExpression
    {
        private IReadOnlyList<T> collection;
        private int tIndex = 0;
        private int tLen;
        private Stack<Layer> Layers = new Stack<Layer>();

        private class Layer
        {
            protected internal IExpressionProvider<T> expressionProvider;
            protected internal List<IExpression> expressions = new List<IExpression>();
            protected internal int depth;

            protected internal Layer() { }
            public Layer(IExpressionProvider<T> expressionProvider, int depth)
            {
                this.expressionProvider = expressionProvider ?? throw new ArgumentNullException(nameof(expressionProvider));
                this.depth = depth;
            }

            public IExpressionProvider<T> ExpressionProvider => expressionProvider;

            public List<IExpression> Expressions => expressions;
        }


        public Submarine(IReadOnlyList<T> enumerator)
        {
            collection = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
            tLen = collection.Count;
        }

        private Submarine(IReadOnlyList<T> collection, int tIndex) : this(collection)
        {
            this.tIndex = tIndex;
        }

        #region IEnumerator<T> Support

        public T Current => collection[tIndex];
        object IEnumerator.Current => collection[tIndex];

        public void Dispose()
        {
            Layers.Clear();
        }

        public bool MoveNext()
        {
            if (Layers.Count <= 0) return ++tIndex >= tLen;
            var layer = Layers.Peek();
            layer.Expressions.Add(Current);

            return ++tIndex >= tLen;
        }

        public void Reset()
        {
            tIndex = 0;
        }

        #endregion

        public void Prepare(IExpressionProvider<T> provider)
        {
            Layer l = new Layer(provider, Layers.Count);
            Layers.Push(l);
        }

        public void Discard()
        {
            Layers.Pop();
        }

        public Expression Collapse()
        {
            Layer layer = Layers.Pop();
            var e = layer.ExpressionProvider.GetExpression(layer.Expressions);
            return e;
        }

        public IExpressionBuilder<T> Split()
        {
            var sub = new Submarine<T>(collection, tIndex);

            Stack<Layer> reStack = new Stack<Layer>();
            foreach (var item in Layers)
            {
                Layer l = new Layer()
                {
                    expressionProvider = item.expressionProvider,
                    expressions = item.expressions.ToList(),
                    depth = item.depth
                };
                reStack.Push(l);
            }

            foreach (var item in reStack)
            {
                sub.Layers.Push(item);
            }

            return sub;
        }

        public void Join(IExpressionBuilder<T> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (builder is not Submarine<T> sub) throw new ArgumentException("Cannot join " + nameof(builder) + " to this collection");

            tIndex = sub.tIndex;
            Layers = sub.Layers;
        }


        public void Collect(Expression expression)
        {
            Layer layer = Layers.Peek();
            layer.Expressions.Add(expression);
        }
    }
}
