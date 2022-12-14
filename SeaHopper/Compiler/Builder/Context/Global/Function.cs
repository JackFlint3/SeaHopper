using SeaHopper.Compiler.Trees.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Builder.Context.Global
{
    public class Function
    {
        public Namespace? Parent { get; set; }

        public string Identifier { get; private set; }

        public string? Body { get; private set; }
        private bool built = false;
        // The expression that needs to be built from
        private StatementHolder body;


        public Function(string identifier, StatementHolder body)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            this.body = body;
        }

        public void Build(BuildContext context)
        {
            if (built) throw new Exception("Function already built");


            context.Data["prefix"] = null;
            context.CurrentFunction = this;
            context.CurrentNamespace = this.Parent;
            this.Body = this.body.Build(context);


            built = true;
        }

        internal void AppendBody(string appendable)
        {
            if (!built) throw new Exception("Function not yet built");
            this.Body += "\n" + appendable;
        }
    }
}
