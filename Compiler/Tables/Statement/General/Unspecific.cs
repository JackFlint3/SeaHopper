using static MCDatapackCompiler.Compiler.Parser.Trees.Syntax.StatementDiagram;

namespace MCDatapackCompiler.Compiler.Parser.Trees.Syntax.General
{
    public abstract partial class Unspecific : Statement
    {

        /// <summary>
        /// Passively declared types for use in active declarations
        /// </summary>
        public abstract partial class JEArgumentTypes : Unspecific
        {
            public abstract partial class Minecraft : JEArgumentTypes
            {
                //public class UUID : Minecraft
                //{
                //    public override Pattern<LexerToken> Pattern =>
                //        Patterns.All(new() {
                //            Patterns.Literals.UUID
                //        });
                //}
            }
            public abstract partial class Parameters : JEArgumentTypes
            {
            }
        }
    }
}
