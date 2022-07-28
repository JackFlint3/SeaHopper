using SeaHopper.Compiler.Builder;
using SeaHopper.Compiler.Trees.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeaHopper.Compiler.Trees.Expressions.Expression;

namespace SeaHopper.Compiler.Parser
{
    internal partial class Submarine<T> : IExpressionBuilder<T> where T : IBuildable
    {
        private IReadOnlyList<T> collection;
        private int tIndex = 0;
        private int tLen;
        private Stack<Layer> Layers = new Stack<Layer>();


        public Submarine(IReadOnlyList<T> enumerator)
        {
            collection = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
            tLen = collection.Count;
        }

        private Submarine(IReadOnlyList<T> collection, int tIndex) : this(collection)
        {
            this.tIndex = tIndex;
        }

        #region LayerControls
        /// <summary>
        /// Prepares submarine for diving deeper
        /// </summary>
        /// <param name="provider"></param>
        public void Prepare(IExpressionProvider<T> provider)
        {
            Layer l = new Layer(provider, Layers.Count);
            Layers.Push(l);
        }

        /// <summary>
        /// Adds an expression to the current layer
        /// </summary>
        /// <param name="expression"></param>
        public void Collect(Expression expression)
        {
            Layer layer = Layers.Peek();
            layer.Expressions.Add(expression);
        }

        /// <summary>
        /// Removes a failed layer
        /// </summary>
        public void Discard()
        {
            Layers.Pop();
        }

        /// <summary>
        /// Extracts an expression out the current layer
        /// </summary>
        /// <returns></returns>
        public Expression Collapse()
        {
            Layer layer = Layers.Pop();
            var e = layer.ExpressionProvider.GetExpression(layer.Expressions);
            return e;
        }
        #endregion


        /// <summary>
        /// Creates a copy of the current Path
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Rejoins a copy of the Path with the Current Path
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Join(IExpressionBuilder<T> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (builder is not Submarine<T> sub) throw new ArgumentException("Cannot join " + nameof(builder) + " to this collection");

            tIndex = sub.tIndex;
            Layers = sub.Layers;
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

        public override string ToString()
        {
            string current = "";
            try
            {
                current = this.Current + "";
            }
            catch (Exception)
            {
                // Discard as its not important
            }

            return "Submarine(" + current + ")";
        }
    }
}
