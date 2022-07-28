using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaHopper.Compiler.Builder
{
    public interface IBuildable
    {
        string Build();
        string Build(Context.BuildContext context);
    }
}
